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

    bool isGameRunning;
    public bool IsGameRunning { get => isGameRunning; }

    public InGameRoom()
    {
        player = new Dictionary<int, Player>();
    }

    public void GameStart(Dictionary<int, GameRoomUser> _allUsers)
    {
        allUsers = _allUsers;
        blueTeamScore = 0;
        redTeamScore = 0;
        isGameRunning = true;

        player.Clear();

        Thread.Sleep(100);
        SpawnPlayer();

        ItemBoxGenerator.Instance.Test();
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

            if (blueTeamScore >= 1)
            {
                EndGame(false);
            }
        }
        else
        {
            ++redTeamScore;

            if (redTeamScore >= 1)
            {
                EndGame(true);
            }
        }

        ServerSend.ScoreUpdate(blueTeamScore, redTeamScore);
    }

    public void EndGame(bool _isRedWin)
    {
        if (!isGameRunning) return;
        isGameRunning = false;

        Debug.Log("EndGame - " + (_isRedWin ? "Red Team" : "Blue Team") + "Win");

        // 모든 플레이어 삭제
        foreach (Player _player in player.Values)
        {
            Server.clients[_player.id].player = null;
            GameObject.Destroy(_player.gameObject);
        }

        player.Clear();

        foreach (GameRoomUser _gameRoomUser in allUsers.Values)
        {
            _gameRoomUser.SetReady(false);
        }

        ServerSend.EndGame(_isRedWin);

        Projectile.Clear();
    }
}
