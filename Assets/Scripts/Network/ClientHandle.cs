using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.Instance == null)
        {
            Debug.Log("1@@");
        }

        if (_username == null)
        {
            Debug.Log("2@@");
        }

        if (_position == null)
        {
            Debug.Log("3@@");

        }

        if (_rotation == null)
        {
            Debug.Log("4@@");
        }

        GameManager.Instance.SpawnPlayer(_id, _username, _position, _rotation);
    }
}
