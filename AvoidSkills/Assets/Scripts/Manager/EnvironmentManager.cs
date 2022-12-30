using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager instance;
    public static EnvironmentManager Instance { get => instance; }

    public float itemBoxSpawnInterval;
    public float itemBoxDestroyTime;
    public int itemBoxLevelUpInterval;
    public SkillCode limitSkillCode;

    [SerializeField]
    private GameObject itemBoxPrefab;
    public GameObject itemBallPrefab;

    private Vector3 spawnArea;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            spawnArea = GameObject.Find("Ground").transform.localScale;

            StartCoroutine(SpawnItemBoxCoroutine());
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private IEnumerator SpawnItemBoxCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnvironmentManager.Instance.itemBoxSpawnInterval);
            SpawnItemBox();
        }
    }

    private void SpawnItemBox()
    {
        System.Random random = new System.Random();

        int halfWidth = (int)(spawnArea.x * 10) / 2;
        int halfHeight = (int)(spawnArea.z * 10) / 2;

        Vector3 randomPosLeft = new Vector3(random.Next(-halfWidth, 0), 1, random.Next(-halfHeight, halfHeight));
        Vector3 randomPosRight = new Vector3(random.Next(0, halfWidth), 1, random.Next(-halfHeight, halfHeight));

        GameObject ItemBoxInLeftArea = Instantiate(itemBoxPrefab, randomPosLeft, Quaternion.identity);
        GameObject ItemBoxInRightArea = Instantiate(itemBoxPrefab, randomPosRight, Quaternion.identity);
    }

}
