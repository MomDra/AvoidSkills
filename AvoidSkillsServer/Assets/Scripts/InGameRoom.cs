using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class InGameRoom
{
    private bool isStarted;

    private Dictionary<int, GameRoomUser> allUsers;
    private Dictionary<int, Player> player;

    int blueTeamScore;
    int redTeamScore;

    public InGameRoom()
    {
        player = new Dictionary<int, Player>();
    }

    public void GameStart(Dictionary<int, GameRoomUser> _allUsers)
    {
        allUsers = _allUsers;
        blueTeamScore = 0;
        redTeamScore = 0;

        Thread.Sleep(100);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        foreach (GameRoomUser _roomUser in allUsers.Values)
        {
            Player _player = NetworkManager.Instance.InstantiatePlayer();
            _player.Initialize(_roomUser.id, _roomUser.userName);

            player.Add(_roomUser.id, _player);

            Server.clients[_roomUser.id].player = _player;
        }

        foreach (GameRoomUser _roomUser in allUsers.Values)
        {
            foreach (Player _player in player.Values)
            {
                ServerSend.SpawnPlayer(_roomUser.id, _player);
            }
        }
    }

    public void DeadPlayer(int _userId)
    {
        if (allUsers[_userId].isRed)
        {
            ++blueTeamScore;
        }
        else
        {
            ++redTeamScore;
        }

        ServerSend.ScoreUpdate(blueTeamScore, redTeamScore);
    }
}
