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
    private Vector3 spawnArea;

    private static float WORLDTIME = 0;
    private float spawnTimer = 0;

    void Awake()
    {
        
        spawnArea = field.transform.localScale;
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
        System.Random random = new System.Random();

        int halfWidth = (int)(spawnArea.x * 10) / 2;
        int halfHeight = (int)(spawnArea.z * 10) / 2;

        Vector3 randomPosLeft = new Vector3(random.Next(-halfWidth, 0), 1, random.Next(-halfHeight, halfHeight));
        Vector3 randomPosRight = new Vector3(random.Next(0, halfWidth), 1, random.Next(-halfHeight, halfHeight));

        GameObject ItemBoxInLeftArea = Instantiate(itemBoxPrefab, randomPosLeft, Quaternion.identity);
        GameObject ItemBoxInRightArea = Instantiate(itemBoxPrefab, randomPosRight, Quaternion.identity);
    }
}
