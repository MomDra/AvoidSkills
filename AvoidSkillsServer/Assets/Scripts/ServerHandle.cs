using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}, UserName: {_username}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }

        // send player into game
        // Server.clients[_fromClient].SendIntoGame(_username);

        GameRoomUser gameRoomUser = new GameRoomUser(_fromClient, _username);
        Server.gameRoom.AddUser(gameRoomUser);
    }

    public static void PlayerTargetPosition(int _fromClient, Packet _packet)
    {
        Vector3 _targetPos = _packet.ReadVector3();

        Server.clients[_fromClient].player.SetTargetPos(_targetPos);
    }

    public static void ShootSkill(int _fromClient, Packet _packet)
    {
        SkillCode _skillCode = (SkillCode)_packet.ReadInt();
        SkillLevel _skillLevel = (SkillLevel)_packet.ReadInt();
        Vector3 _mousePos = _packet.ReadVector3();
        bool _isItemSkill = _packet.ReadBool();

        Server.clients[_fromClient].player.ShootSkill(_skillCode, _skillLevel, _mousePos, _isItemSkill);
    }

    public static void ReadyButton(int _fromClient, Packet _packet)
    {
        bool _isReady = _packet.ReadBool();

        Server.gameRoom.SetReady(_fromClient, _isReady);
    }

    public static void StartButton(int _fromClient, Packet _packet)
    {
        bool _start = _packet.ReadBool();

        Server.gameRoom.StartGame(_start);
    }

    public static void testLabStartButton(int _fromClient, Packet _packet){
        bool _testLabStart = _packet.ReadBool();

        Server.gameRoom.StartTestLab(_testLabStart);
    }

    public static void InGameSceneLoaded(int _fromClient, Packet _packet)
    {
        bool _isLoaded = _packet.ReadBool();

        Server.gameRoom.SetInGameSceneLoaded(_fromClient, _isLoaded);
    }

    public static void WaitingRoomSceneLoaded(int _fromClient, Packet _packet)
    {
        bool _isLoaded = _packet.ReadBool();


    }
}
