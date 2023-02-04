using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxGenerator : MonoBehaviour
{
    private static ItemBoxGenerator instance;
    public static ItemBoxGenerator Instance { get => instance; }

    [SerializeField]
    private GameObject itemBoxPrefab;
    [SerializeField]
    private float spawnItemBoxInterval;

    private float borderX = 19.5f;
    private float borderZ = 11.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("Destroy obejct");
        }
    }

    public void GenerateStart()
    {
        StartCoroutine(SpawnItemBoxCoroutine());
    }

    private IEnumerator SpawnItemBoxCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnItemBoxInterval);

            SpawnItemBox();
        }
    }

    public void SpawnItemBox()
    {
        SpawnRedItemBox();
        SpawnBlueItemBox();
    }

    private void SpawnRedItemBox()
    {
        float randomX = Random.Range(1.5f, borderX);
        float randomZ = Random.Range(-borderZ, borderZ);

        Vector3 spawnPos = new(randomX, 0.5f, randomZ);

        Instantiate(itemBoxPrefab, spawnPos, Quaternion.identity).GetComponent<ItemBox>().Initialize(true);
    }

    private void SpawnBlueItemBox()
    {
        float randomX = Random.Range(-borderX, -1.5f);
        float randomZ = Random.Range(-borderZ, borderZ);

        Vector3 spawnPos = new(randomX, 0.5f, randomZ);

        Instantiate(itemBoxPrefab, spawnPos, Quaternion.identity).GetComponent<ItemBox>().Initialize(false);
    }
}
