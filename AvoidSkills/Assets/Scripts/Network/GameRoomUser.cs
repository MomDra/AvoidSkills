using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomUser
{
    public int id { get; private set; }
    public string userName { get; private set; }
    public bool isRed { get; private set; }
    public bool isRoomKing { get; private set; }

    public GameRoomUser(int _id, string _userName, bool _isRed, bool _isRoomKing)
    {
        id = _id;
        userName = _userName;
        isRed = _isRed;
        isRoomKing = _isRoomKing;
    }
}
