using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend : MonoBehaviour
{
    public static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength(); // 맨 앞에 얼마의 데이터가 있는지 int 정수 데이터 크기 값 삽입하는 거임
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; ++i)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }

    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; ++i)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; ++i)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }

    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; ++i)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }

    #region Packets
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);
            _packet.Write(_player.transform.rotation);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void PlayerPosition(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.position);

            SendUDPDataToAll(_packet);
        }
    }

    public static void PlayerRotation(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.rotation);

            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void playerDisconnected(int _playerId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
        {
            _packet.Write(_playerId);
            SendTCPDataToAll(_packet);
        }
    }

    public static void PlayerHealth(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerHealth))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.status.hp);

            SendTCPDataToAll(_packet);
        }
    }

    public static void PlayerRespawned(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerRespawned))
        {
            _packet.Write(_player.id);

            SendTCPDataToAll(_packet);
        }
    }

    public static void ItemSpawned(int _spawnerId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.itemSpawned))
        {
            _packet.Write(_spawnerId);

            SendTCPDataToAll(_packet);
        }
    }

    public static void ItemPickedUp(int _spawnerId, int _byPlayer)
    {
        using (Packet _packet = new Packet((int)ServerPackets.itemPickedUp))
        {
            _packet.Write(_spawnerId);
            _packet.Write(_byPlayer);

            SendTCPDataToAll(_packet);
        }
    }

    public static void SpawnProjectile(Projectile _projectile, int _thrownByPlaeyr)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnProjectile))
        {
            _packet.Write(_projectile.id);
            _packet.Write(_projectile.transform.position);
            _packet.Write(_thrownByPlaeyr);

            _packet.Write((int)_projectile.skillCode);
            _packet.Write((int)_projectile.skillLevel);

            SendTCPDataToAll(_packet);
        }
    }

    public static void ProjectilePosition(Projectile _projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.projectilePosition))
        {
            _packet.Write(_projectile.id);
            _packet.Write(_projectile.transform.position);
            _packet.Write(_projectile.transform.rotation);

            SendTCPDataToAll(_packet);
        }
    }

    public static void ProjectileExploded(Projectile _projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.projectileExploded))
        {
            _packet.Write(_projectile.id);
            _packet.Write(_projectile.transform.position);

            SendTCPDataToAll(_packet);
        }
    }

    public static void DestoryProjectile(Projectile _projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.destoryProjectile))
        {
            _packet.Write(_projectile.id);

            SendTCPDataToAll(_packet);
        }
    }

    public static void PlayerStatus(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerStatus))
        {
            _packet.Write((int)_player.status.state);
            _packet.Write(_player.status.hp);
            _packet.Write(_player.status.maxHp);
            _packet.Write(_player.status.armor);
            _packet.Write(_player.status.moveSpeed);

            SendTCPData(_player.id, _packet);
        }
    }

    public static void AddMember(int _toClient, GameRoomUser _gameRoomUser)
    {
        using (Packet _packet = new Packet((int)ServerPackets.addMember))
        {
            _packet.Write(_gameRoomUser.id);
            _packet.Write(_gameRoomUser.userName);
            _packet.Write(_gameRoomUser.isRed);
            _packet.Write(_gameRoomUser.isRoomKing);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void RemoveMember(int _userId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.removeMember))
        {
            _packet.Write(_userId);

            SendTCPDataToAll(_packet);
        }
    }

    public static void RoomKing(int _roomKingId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.roomKing))
        {
            _packet.Write(_roomKingId);

            SendTCPDataToAll(_packet);
        }
    }

    public static void UserReady(int _userId, bool _isReady)
    {
        using (Packet _packet = new Packet((int)ServerPackets.userReady))
        {
            _packet.Write(_userId);
            _packet.Write(_isReady);

            SendTCPDataToAll(_packet);
        }
    }

    public static void StartGame()
    {
        using (Packet _packet = new Packet((int)ServerPackets.startGame))
        {
            _packet.Write(true);

            SendTCPDataToAll(_packet);
        }
    }

    public static void ScoreUpdate(int _blueTeamScore, int _redTeamScore)
    {
        using (Packet _packet = new Packet((int)ServerPackets.scoreUpdate))
        {
            _packet.Write(_blueTeamScore);
            _packet.Write(_redTeamScore);

            SendTCPDataToAll(_packet);
        }
    }

    public static void EndGame(bool _isRedWin)
    {
        using (Packet _packet = new Packet((int)ServerPackets.endGame))
        {
            _packet.Write(_isRedWin);

            SendTCPDataToAll(_packet);
        }
    }

    public static void RoomKingModelOnly(int _roomKingId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.roomKingModelOnly))
        {
            _packet.Write(_roomKingId);

            SendTCPDataToAll(_packet);
        }
    }

    public static void AddMemberModelOnly(int _toClient, GameRoomUser _gameRoomUser)
    {
        using (Packet _packet = new Packet((int)ServerPackets.addMemberModelOnly))
        {
            _packet.Write(_gameRoomUser.id);
            _packet.Write(_gameRoomUser.userName);
            _packet.Write(_gameRoomUser.isRed);
            _packet.Write(_gameRoomUser.isRoomKing);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void RemoveMemberModelOnly(int _userId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.removeMemberModelOnly))
        {
            _packet.Write(_userId);

            SendTCPDataToAll(_packet);
        }
    }

    #endregion
}
