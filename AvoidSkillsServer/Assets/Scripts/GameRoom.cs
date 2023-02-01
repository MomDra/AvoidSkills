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

    public void RemoveUser(int _userId)
    {
        if (numUser <= 0) throw new System.Exception("user의 인원이 0보다 작아지려 하고 있습니다.");
        bool removeUserIsRed = allUsers[_userId].isRed;

        allUsers.Remove(_userId);

        if (roomKing.id == _userId)
        {
            ResetRoomKing(inGameRoom.IsGameRunning);
        }


        if (inGameRoom.IsGameRunning)
        {
            ServerSend.RemoveMemberModelOnly(_userId);
        }
        else
        {
            ServerSend.RemoveMember(_userId);
        }

        --numUser;

        if (removeUserIsRed)
        {
            --numRedUser;
            if (numRedUser == 0) inGameRoom.EndGame(false);
        }
        else
        {
            --numblueUser;
            if (numblueUser == 0) inGameRoom.EndGame(true);
        }






    }

    public void AddUser(GameRoomUser _user)
    {
        if (numUser >= 4) throw new System.Exception("user의 최대 인원을 초과하려 하고 있습니다.");

        allUsers.Add(_user.id, _user);
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
            if (inGameRoom.IsGameRunning)
            {
                ServerSend.AddMemberModelOnly(_roomUser.id, _user);
            }
            else
            {
                ServerSend.AddMember(_roomUser.id, _user);
            }


            if (_roomUser != _user)
            {
                if (inGameRoom.IsGameRunning)
                {
                    ServerSend.AddMemberModelOnly(_user.id, _roomUser);
                }
                else
                {
                    ServerSend.AddMember(_user.id, _roomUser);
                }
            }
        }

        if (numUser == 1)
        {
            SetRoomking(_user, false);
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
                if (_roomUser.isRoomKing) continue;
                _isAllReady = false;
                Debug.Log($"{_roomUser.id} doesn't ready, {_roomUser.isReady}");
                break;
            }
        }

        if (_isAllReady && numUser >= 2)
        {
            ServerSend.StartGame();
            inGameRoom.GameStart(allUsers);
        }
        else
        {
            Debug.Log("Every user doesn't ready or num of user is only one");
        }
    }

    private void ResetRoomKing(bool _isModelOnly)
    {
        if (numUser >= 1)
        {
            foreach (GameRoomUser _roomUser in allUsers.Values)
            {
                SetRoomking(_roomUser, _isModelOnly);
                return;
            }
        }
        else
        {
            SetRoomking(null, _isModelOnly);
        }
    }

    private void SetRoomking(GameRoomUser _user, bool _isModelOnly)
    {
        Debug.Log("SetRoomking");

        roomKing = _user;
        if (_user != null)
        {
            _user.SetRoomking(true);
            _user.SetReady(true);
            Debug.Log($"now {_user.id} is roomking, {_user.isReady}");


            if (_isModelOnly)
            {
                ServerSend.RoomKingModelOnly(roomKing.id);
            }
            else
            {
                ServerSend.RoomKing(roomKing.id);
            }
        }
        else
        {
            Debug.Log("now roomking is null");
        }
    }

    public void SetInGameSceneLoaded(int _userId, bool _isLoaded)
    {
        allUsers[_userId].SetInGameSceneLoaded(_isLoaded);
    }
}
