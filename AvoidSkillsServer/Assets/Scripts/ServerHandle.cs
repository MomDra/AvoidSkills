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
        Server.clients[_fromClient].SendIntoGame(_username);
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

        Server.clients[_fromClient].player.ShootSkill(_skillCode, _skillLevel, _mousePos);
    }
}