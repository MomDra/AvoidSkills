using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRoom
{
    private bool isStarted;

    private GameRoomUser[] allUsers;
    private Player[] players;

    public InGameRoom()
    {
        players = new Player[4];
    }

    public void GameStart(GameRoomUser[] _allUsers)
    {
        allUsers = _allUsers;

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null)
            {
                Player _player = NetworkManager.Instance.InstantiatePlayer();
                _player.Initialize(allUsers[i].id, allUsers[i].userName);

                players[i] = _player;

                Server.clients[allUsers[i].id].player = _player;
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (allUsers[i] != null)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (players[j] != null)
                    {
                        ServerSend.SpawnPlayer(allUsers[i].id, players[j]);
                    }
                }
            }
        }
    }
}
