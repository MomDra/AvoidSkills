using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoom
{
    public GameRoomUser[] blueUsers { get; private set; }

    public GameRoomUser[] redUsers { get; private set; }

    public Dictionary<int, GameRoomUser> allUsers { get; private set; }
    public int numUser { get; private set; }
    public int numblueUser { get; private set; }
    public int numRedUser { get; private set; }

    private GameRoomUser roomKing;
    public InGameRoom inGameRoom;

    public GameRoom()
    {
        blueUsers = new GameRoomUser[2];
        redUsers = new GameRoomUser[2];
        allUsers = new Dictionary<int, GameRoomUser>();

        inGameRoom = new InGameRoom();

        numUser = 0;
        numblueUser = 0;
        numRedUser = 0;
    }


    public void InitGame()
    {

    }


    public void StartGame()
    {

    }

    public void RemoveUser(int _userId)
    {
        if (numUser <= 0) throw new System.Exception("user의 인원이 0보다 작아지려 하고 있습니다.");

        --numUser;
        if (allUsers[_userId].isRed)
        {
            --numRedUser;
        }
        else
        {
            --numblueUser;
        }
        allUsers.Remove(_userId);

        if (roomKing.id == _userId)
        {
            ResetRoomKing();
        }

        ServerSend.RemoveMember(_userId);
    }

    public void AddUser(GameRoomUser _user)
    {
        if (numUser >= 4) throw new System.Exception("user의 최대 인원을 초과하려 하고 있습니다.");

        if (numUser == 0)
        {
            SetRoomking(_user);
        }

        allUsers.Add(_user.id, _user); // 리펙토링
        ++numUser;

        if (numblueUser > numRedUser)
        {
            redUsers[numRedUser++] = _user;
            _user.SetTeam(true);
        }
        else
        {
            blueUsers[numblueUser++] = _user;
            _user.SetTeam(false);
        }

        foreach (GameRoomUser _roomUser in allUsers.Values)
        {
            ServerSend.AddMember(_roomUser.id, _user);

            if (_roomUser != _user)
            {
                ServerSend.AddMember(_user.id, _roomUser);
            }
        }
    }

    public void SetReady(int _userId, bool _isReady)
    {
        allUsers[_userId].SetReady(_isReady);

        ServerSend.UserReady(_userId, _isReady);
    }

    public void StartGame(bool _start)
    {
        bool _isAllReady = true;

        foreach (GameRoomUser _roomUser in allUsers.Values)
        {
            if (_roomUser.isReady == false)
            {
                _isAllReady = false;
                break;
            }
        }

        if (_isAllReady)
        {
            ServerSend.StartGame();
            inGameRoom.GameStart(allUsers);
        }
        else
        {
            Debug.Log("Every user doesn't ready");
        }
    }

    private void ResetRoomKing()
    {
        if (numUser >= 1)
        {
            foreach (GameRoomUser _roomUser in allUsers.Values)
            {
                SetRoomking(_roomUser);
                return;
            }
        }
        else
        {
            SetRoomking(null);
        }
    }

    private void SetRoomking(GameRoomUser _user)
    {
        roomKing = _user;
        if (_user != null)
        {
            _user.SetRoomking(true);
            _user.SetReady(true);
            Debug.Log($"now {_user.id} is roomking");

            ServerSend.RoomKing(roomKing.id);
        }
        else
        {
            Debug.Log("now roomking is null");
        }
    }
}
