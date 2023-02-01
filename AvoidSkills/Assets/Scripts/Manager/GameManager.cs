using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    public static Dictionary<int, ItemBoxManager> itemBoxes = new Dictionary<int, ItemBoxManager>();
    public static Dictionary<int, ItemBallManager> itemBalls = new Dictionary<int, ItemBallManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemBoxPrefab;
    public GameObject itemBallPrefab;

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

    public void InstantiateProjectile(int _id, Vector3 _position, SkillCode _skillCode, SkillLevel _skillLevel)
    {
        GameObject _projectile = Instantiate(SkillDB.Instance.GetSkillPrefab(_skillCode, _skillLevel), _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);

        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }

    public void InstantiateItemBox(int _id, Vector3 _position){
        GameObject _itemBox = Instantiate(itemBoxPrefab, _position, Quaternion.identity);
        _itemBox.GetComponent<ItemBoxManager>().Initialize(_id);

        itemBoxes.Add(_id, _itemBox.GetComponent<ItemBoxManager>());
    }

    public void LevelUpItemBox(int _id)
    {
        itemBoxes[_id].LevelUpdate();
    }

    public void DestroyItemBox(int _id){
        itemBoxes[_id].Destory();
    }

    public void InstantiateItemBall(int _id, Vector3 _position){
        GameObject _itemBall = Instantiate(itemBallPrefab, _position, Quaternion.identity);
        _itemBall.GetComponent<ItemBallManager>().Initialize(_id);

        itemBalls.Add(_id, _itemBall.GetComponent<ItemBallManager>());
    }

    public void DestroyItemBall(int _id){
        itemBalls[_id].Destory();
    }

    public void GainItemBall(SkillCode _skillCode, SkillLevel _skillLevel){
        //
    }

    public static void ClearInGameData()
    {
        players.Clear();
        projectiles.Clear();
    }

}

// 서버 itemBox : id, level, team, position, itemBall, skill