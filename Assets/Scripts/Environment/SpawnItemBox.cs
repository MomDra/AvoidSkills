using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnItemBox : MonoBehaviour
{
    [SerializeField]
    private GameObject field;

    [SerializeField]
    private GameObject itemBoxPrefab;
    [SerializeField]
    private int spawnInterval;
    private Vector3 spawnRange;

    private static float WORLDTIME = 0;
    private float spawnTimer = 0;

    void Awake()
    {
        spawnRange = field.transform.localScale;
    }

    void Update()
    {
        WORLDTIME += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if((int)spawnTimer == spawnInterval){
            spawnTimer = 0;
            spawnItemBox();
        }
    }

    private void spawnItemBox(){
        GameObject clone = Instantiate(itemBoxPrefab, getRandomPos(), Quaternion.identity);
    }

    private Vector3 getRandomPos(){
        System.Random random = new System.Random();
        return new Vector3(random.Next(0, (int)spawnRange.x * 10), 1, random.Next(0, (int)spawnRange.y * 10));
    }
}
