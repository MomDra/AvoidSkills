using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;

public class ClientHandle
{

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.Instance.MyId = _myId;

        Client.Instance.udp.Connect(((IPEndPoint)Client.Instance.tcp.socket.Client.LocalEndPoint).Port);


        SceneManager.LoadScene(1);

        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        Debug.Log("spawn");

        GameManager.Instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPositionUpdate(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        Debug.Log(_position);
        try
        {
             GameManager.players[_id].transform.position = _position;
        }
        catch (System.Exception)
        {
            
        }
    }

    public static void PlayerRotationUpdate(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        GameObject.Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void SetPlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int _health = _packet.ReadInt();

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
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.players[_byPlayer].itemCount++;
    }

    public static void InstantiateProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        SkillCode _skillCode = (SkillCode)_packet.ReadInt();
        SkillLevel _skillLevel = (SkillLevel)_packet.ReadInt();

        GameManager.Instance.InstantiateProjectile(_projectileId, _position, _skillCode, _skillLevel);
    }

    public static void ProjectilePositionUpdate(Packet _packet)
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

    public static void ProjectileDestroyed(Packet _packet)
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

    public static void AddMember(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _userName = _packet.ReadString();
        bool _isRed = _packet.ReadBool();
        bool _isRoomKing = _packet.ReadBool();

        Debug.Log($"{_userName}: {_id} - {_isRed}");

        GameRoomUser gameRoomUser = new GameRoomUser(_id, _userName, _isRed, _isRoomKing);

        MemberUIView.Instance.AddMember(gameRoomUser);
    }

    public static void RemoveMember(Packet _packet)
    {
        int _userId = _packet.ReadInt();

        Debug.Log("_userId 나갔음" + _userId);

        MemberUIView.Instance.RemoveMember(_userId);
    }

    public static void RoomKingUpdate(Packet _packet)
    {
        int _roomKingId = _packet.ReadInt();


        MemberUIView.Instance.CrownImageUpdate(_roomKingId);
        ReadyStartUIView.Instance.ReadyStartTextUpdate(Client.Instance.MyId == _roomKingId);
    }

    public static void UserReady(Packet _packet)
    {
        int _userId = _packet.ReadInt();
        bool _isReady = _packet.ReadBool();

        MemberUIView.Instance.CheckImageUpdate(_userId, _isReady);
    }
    
    public static void StartGame(Packet _packet){
        bool _isStart = _packet.ReadBool();
        
        SceneManager.LoadScene(2); 
    }

    public static void ItemBallPositionUpdate(){
        
    }





    
}
