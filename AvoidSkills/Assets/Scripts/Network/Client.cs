using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    private static Client instance;
    public static Client Instance { get => instance; }
    private static int dataBufferSize = 4096;

    [SerializeField]
    private string ip = "127.0.0.1";
    [SerializeField]
    private int port = 26950;
    [SerializeField]
    private int myId = 0;
    public int MyId
    {
        get => myId;
        set
        {
            myId = value;
            Debug.Log($"Clinet Id가 {value}로 설정되었습니다.");
        }
    }
    public string UserName { get; private set; }
    public bool isRed
    {
        get => isRed;
        set
        {
            isRed = value;
            Debug.Log($"Clinet Team가 {value}로 설정되었습니다.");
        }
    }

    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            IntitializeClientData();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects!"); ;
            Destroy(this);
        }
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer(string _ip, string _userName)
    {
        ip = _ip;
        UserName = _userName;
        tcp = new TCP();
        udp = new UDP();
        tcp.Connect();
    }

    private void IntitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.welcome, ClientHandle.Welcome},
            {(int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer},
            {(int)ServerPackets.playerPosition, ClientHandle.PlayerPositionUpdate},
            {(int)ServerPackets.playerRotation, ClientHandle.PlayerRotationUpdate},
            {(int)ServerPackets.playerDisconnected, ClientHandle.PlayerDisconnected},
            {(int)ServerPackets.playerHealth, ClientHandle.SetPlayerHealth},
            {(int)ServerPackets.playerRespawned, ClientHandle.PlayerRespawned},
            {(int)ServerPackets.itemSpawned, ClientHandle.ItemSpawned},
            {(int)ServerPackets.itemPickedUp, ClientHandle.ItemPickedUp},
            {(int)ServerPackets.spawnProjectile, ClientHandle.InstantiateProjectile},
            {(int)ServerPackets.projectilePosition, ClientHandle.ProjectilePositionUpdate},
            {(int)ServerPackets.projectileExploded, ClientHandle.ProjectileExploded},
            {(int)ServerPackets.destoryProjectile, ClientHandle.ProjectileDestroyed},
            {(int)ServerPackets.addMember, ClientHandle.AddMember},
            {(int)ServerPackets.removeMember, ClientHandle.RemoveMember},
            {(int)ServerPackets.roomKing, ClientHandle.RoomKingUpdate},
            {(int)ServerPackets.userReady, ClientHandle.UserReady},
            {(int)ServerPackets.startGame, ClientHandle.StartGame},
            {(int)ServerPackets.scoreUpdate, ClientHandle.ScoreUpdate},
            {(int)ServerPackets.endGame, ClientHandle.EndGame}
    };

        Debug.Log("Initialized packets.");
    }

    public void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Close();
            udp.socket.Close();

            Debug.Log("Disconnected form server");
        }
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private Packet receiveData;
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                Debug.Log("can't connect to server");
                return;
            }

            stream = socket.GetStream();

            receiveData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            instance.isConnected = true;
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);

                if (_byteLength <= 0)
                {
                    // 연결 끊기
                    Instance.Disconnect(); // 왜 Client의 disconnect를 호출한 것일까?
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                // 데이터 다루기
                receiveData.Reset(HandleData(_data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving TCP data: {_ex}");
                // 연결 끊기
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;
            receiveData.SetBytes(_data);

            if (receiveData.UnreadLength() >= 4)
            {
                _packetLength = receiveData.ReadInt();
                if (_packetLength <= 0)
                {
                    return true;
                }
            }

            while (_packetLength > 0 && _packetLength <= receiveData.UnreadLength())
            {
                byte[] _packetBytes = receiveData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });

                _packetLength = 0;
                if (receiveData.UnreadLength() >= 4)
                {
                    _packetLength = receiveData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if (_packetLength <= 1)
            {
                return true;
            }

            return false;
        }

        private void Disconnect()
        {
            Instance.Disconnect();
            stream = null;
            receiveData = null;
            receiveBuffer = null;
            socket = null;
        }
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(Instance.ip), Instance.port);
        }

        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);
        }

        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(Instance.myId);
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
                else
                {
                    Debug.Log("udp socket이 null임");
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (_data.Length < 4)
                {
                    Instance.Disconnect();
                    return;
                }

                HandleData(_data);
            }
            catch (Exception _ex)
            {
                // 연결 끊기
                Debug.Log($"UDP 수신 에러로 연결 종료: {_ex}");
                Disconnect();
            }
        }

        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }

        private void Disconnect()
        {
            Instance.Disconnect();

            endPoint = null;
            socket = null;
        }
    }
}

