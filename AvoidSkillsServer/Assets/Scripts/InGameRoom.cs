using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRoom
{
    private bool isStarted;

    private Dictionary<int, GameRoomUser> allUsers;
    private Dictionary<int, Player> player;

    public InGameRoom()
    {
        player = new Dictionary<int, Player>();
    }

    public void GameStart(Dictionary<int, GameRoomUser> _allUsers)
    {
        allUsers = _allUsers;

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
}
