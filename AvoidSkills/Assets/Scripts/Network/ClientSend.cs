using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.Instance.MyId);
            _packet.Write(Client.Instance.UserName);

            SendTCPData(_packet);
            // SendUDPData(_packet); // 서버에 udp 연결을 위한 udp 데이터 전송
        }
    }

    public static void PlayerTargetPosition(Vector3 _targetPos)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerTargetPosition))
        {
            _packet.Write(_targetPos);

            SendTCPData(_packet);
        }
    }

    public static void ShootSkill(SkillCode _skillCode, SkillLevel _skillLevel, Vector3 _mousePos)
    {
        using (Packet _packet = new Packet((int)ClientPackets.shootSkill))
        {
            _packet.Write((int)_skillCode);
            _packet.Write((int)_skillLevel);
            _packet.Write(_mousePos);

            SendTCPData(_packet);
        }
    }
    #endregion
}
