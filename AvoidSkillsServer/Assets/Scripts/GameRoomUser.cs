using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomUser
{
    public int id { get; private set; }
    public string userName { get; private set; }
    public bool isRed;
    public bool isRoomKing;

    public GameRoomUser(int _id, string _userName)
    {
        id = _id;
        userName = _userName;
        isRed = false;
        isRoomKing = false;
    }

    public void SetTeam(bool _isRed)
    {
        isRed = _isRed;
    }
}
