using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ClientHandle : MonoBehaviour
{

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.Instance.MyId = _myId;

        Client.Instance.udp.Connect(((IPEndPoint)Client.Instance.tcp.socket.Client.LocalEndPoint).Port);
        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.Instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        SkillCode _skillCode = (SkillCode)_packet.ReadInt();
        SkillLevel _skillLevel = (SkillLevel)_packet.ReadInt();

        GameManager.Instance.SpawnProjectile(_projectileId, _position, _skillCode, _skillLevel);
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.projectiles[_projectileId].transform.position = _position;
        GameManager.projectiles[_projectileId].transform.rotation = _rotation;
    }

    public static void ProjectileExploded(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.projectiles[_projectileId].Explode(_position);
    }

    public static void DestoryProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();

        GameManager.projectiles[_projectileId].Destory();
        GameManager.projectiles.Remove(_projectileId);
    }

    public static void PlayerStatus(Packet _packet)
    {
        State _state = (State)_packet.ReadInt();
        int _hp = _packet.ReadInt();
        int _maxHp = _packet.ReadInt();
        int _armor = _packet.ReadInt();
        float _moveSpeed = _packet.ReadFloat();
    }
}
