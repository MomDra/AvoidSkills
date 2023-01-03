using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxGenerator : MonoBehaviour
{
    private ItemBoxGenerator instance;

    [SerializeField]
    private float spawnItemBoxInterval;
    [SerializeField]
    private GameObject itemBoxPrefab;
    [SerializeField]
    private Vector2Int minPos;
    [SerializeField]
    private Vector2Int maxPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("awake");
        }
        else if (instance != this)
        {
            Destroy(this);
            Debug.Log("객체가 2개 생성되었습니다. 객체 하나를 삭제합니다.");
        }
    }

    private void Start()
    {
        Debug.Log("start");
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

    private void SpawnItemBox()
    {
        int x = Random.Range(minPos.x, maxPos.x);
        int z = Random.Range(minPos.y, maxPos.y);

        Instantiate(itemBoxPrefab, new Vector3(x, 1f, z), Quaternion.identity);
    }
}
