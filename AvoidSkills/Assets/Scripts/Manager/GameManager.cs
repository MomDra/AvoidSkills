using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            ClientSend.InGameSceneLoaded();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects!"); ;
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;

        if (_id == Client.Instance.MyId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().Initialize(_id, _username, true);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().Initialize(_id, _username, false);
        }


        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem)
    {
        GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
    }

    public void InstantiateProjectile(int _id, Vector3 _position, SkillCode _skillCode, SkillLevel _skillLevel)
    {
        GameObject _projectile = Instantiate(SkillDB.Instance.GetSkillPrefab(_skillCode, _skillLevel), _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);

        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }

    public static void ClearInGameData()
    {
        players.Clear();
        projectiles.Clear();
    }
}
