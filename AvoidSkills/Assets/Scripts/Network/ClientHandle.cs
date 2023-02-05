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

        Debug.Log("Player Spawn: " + _id + " / " + _username + " / " + _position + " / " + _rotation);

        GameManager.Instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPositionUpdate(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

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

    public static void InstantiateSkillObject(Packet _packet)
    {
        int _skillObjectId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Vector3 _localScale = _packet.ReadVector3();
        int _ownPlayerId = _packet.ReadInt();

        SkillCode _skillCode = (SkillCode)_packet.ReadInt();
        SkillLevel _skillLevel = (SkillLevel)_packet.ReadInt();

        GameManager.Instance.InstantiateSkillObject(_skillObjectId, _position, _localScale, _skillCode, _skillLevel);
    }

    public static void SkillObjectPositionUpdate(Packet _packet)
    {
        int _skillObjectId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        Vector3 _localScale = _packet.ReadVector3();

        GameManager.skillObjects[_skillObjectId].transform.position = _position;
        GameManager.skillObjects[_skillObjectId].transform.rotation = _rotation;
    }

    public static void SkillObjectExploded(Packet _packet)
    {
        int _skillObjectId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.skillObjects[_skillObjectId].Explode(_position);
    }

    public static void SkillObjectDestroyed(Packet _packet)
    {
        int _skillObjectId = _packet.ReadInt();

        GameManager.skillObjects[_skillObjectId].Destroy();
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

        MemberModel.Instance.AddMember(new GameUser(_id, _userName, _isRed, _isRoomKing));
    }

    public static void RemoveMember(Packet _packet)
    {
        int _userId = _packet.ReadInt();

        Debug.Log("_userId 나갔음" + _userId);

        MemberModel.Instance.RemoveMember(_userId);
    }

    public static void RoomKingUpdate(Packet _packet)
    {
        int _roomKingId = _packet.ReadInt();

        Debug.Log($"id: {_roomKingId}, RoomKingUpdate 패킷 들어옴");

        MemberModel.Instance.SetRoomKing(_roomKingId);
    }

    public static void UserReady(Packet _packet)
    {
        int _userId = _packet.ReadInt();
        bool _isReady = _packet.ReadBool();

        MemberModel.Instance.SetReady(_userId, _isReady);
    }

    public static void StartGame(Packet _packet)
    {
        bool _isStart = _packet.ReadBool();

        SceneManager.LoadScene(2);
    }

    public static void StartTestLab(Packet _packet)
    {
        bool _isTestLabStart = _packet.ReadBool();

        SceneManager.LoadScene(3);
    }

    public static void ScoreUpdate(Packet _packet)
    {
        int _blueTeamScore = _packet.ReadInt();
        int _redTeamScore = _packet.ReadInt();

        ScoreUIView.Instance.ScoreTextUpdate(_blueTeamScore, _redTeamScore);
    }

    public static void EndGame(Packet _packet)
    {
        bool _isRedTeamWin = _packet.ReadBool();

        ResultUIView.Instance.OpenResultPane(_isRedTeamWin);
    }

    public static void RoomKingModelOnly(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Debug.Log($"{_id} is roomking model only");

        MemberModel.Instance.SetRoomKingModelOnly(_id);
    }

    public static void AddMemberModelOnly(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _userName = _packet.ReadString();
        bool _isRed = _packet.ReadBool();
        bool _isRoomKing = _packet.ReadBool();

        Debug.Log($"{_userName}: {_id} - {_isRed}");

        MemberModel.Instance.AddMemberModelOnly(new GameUser(_id, _userName, _isRed, _isRoomKing));
    }

    public static void RemoveMemberModelOnly(Packet _packet)
    {
        int _userId = _packet.ReadInt();

        Debug.Log("_userId 나갔음" + _userId);

        MemberModel.Instance.RemoveMemberModelOnly(_userId);
    }

    public static void InstantiateItemBox(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _pos = _packet.ReadVector3();

        GameManager.Instance.InstantiateItemBox(_id, _pos);
    }

    public static void LevelUpItemBox(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.Instance.LevelUpItemBox(_id);
    }

    public static void DestroyItemBox(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.Instance.DestroyItemBox(_id);
    }

    public static void InstantiateItemBall(Packet _packet)
    {

        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        SkillCode _skillCode = (SkillCode)_packet.ReadInt();
        SkillLevel _skillLevel = (SkillLevel)_packet.ReadInt();

        Debug.Log($"id: {_id} + ItemBall 생성");
        GameManager.Instance.InstantiateItemBall(_id, _position, _skillCode, _skillLevel);
    }

    public static void ItemBallPositionUpdate(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.itemBalls[_id].transform.position = _position;
        GameManager.itemBalls[_id].transform.rotation = _rotation;
    }

    public static void DestroyItemBall(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.Instance.DestroyItemBall(_id);
    }

    public static void GainItemBall(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Debug.Log($"id: {_id} + GainItemBall 얻었음");

        GameManager.Instance.GainItemBall(_id);
    }
}
