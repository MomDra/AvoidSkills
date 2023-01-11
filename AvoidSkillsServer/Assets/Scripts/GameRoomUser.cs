using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomUser
{
    public int id { get; private set; }
    public string userName { get; private set; }
    public bool isRed { get; private set; }
    public bool isRoomKing { get; private set; }
    public bool isReady { get; private set; }
    public bool isInGameSceneLoaded { get; private set; }

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

    public void SetRoomking(bool _isRoomKing)
    {
        isRoomKing = _isRoomKing;
    }

    public void SetReady(bool _isReady)
    {
        isReady = _isReady;
    }

    public void SetInGameSceneLoaded(bool _isLoaded)
    {
        isInGameSceneLoaded = _isLoaded;
    }
}
