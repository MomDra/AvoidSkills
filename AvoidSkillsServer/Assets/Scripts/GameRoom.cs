using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoom
{
    public GameRoomUser[] blueUsers { get; private set; }

    public GameRoomUser[] redUsers { get; private set; }

    // public GameRoomUser[] allUsers { get; private set; }
    public GameRoomUser[] allUsers { get; private set; }

    public int numUser { get; private set; }
    public int numblueUser { get; private set; }
    public int numRedUser { get; private set; }

    private GameRoomUser roomKing;

    InGameRoom inGameRoom;

    public GameRoom()
    {
        blueUsers = new GameRoomUser[2];
        redUsers = new GameRoomUser[2];
        allUsers = new GameRoomUser[4];

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

        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null)
            {
                if (allUsers[i].id == _userId)
                {
                    if (allUsers[i].isRed)
                    {
                        --numRedUser;
                    }
                    else
                    {
                        --numblueUser;
                    }

                    allUsers[i] = null;
                    break;
                }
            }

        }

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

        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] == null)
            {
                allUsers[i] = _user;
                break;
            }
        }
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

        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null)
            {
                ServerSend.AddMember(allUsers[i].id, _user);
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null && allUsers[i] != _user)
            {
                ServerSend.AddMember(_user.id, allUsers[i]);
            }
        }
    }

    public void SetReady(int _userId, bool _isReady)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null && allUsers[i].id == _userId)
            {
                allUsers[i].SetReady(_isReady);
                break;
            }
        }

        ServerSend.UserReady(_userId, _isReady);
    }

    public void StartGame(bool _start)
    {
        bool _isAllReady = true;
        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null && allUsers[i].isReady == false)
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
            for (int i = 0; i < 4; ++i)
            {
                if (allUsers[i] != null)
                {
                    SetRoomking(allUsers[i]);
                    return;
                }
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
