using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnItemBox : MonoBehaviour
{
    [SerializeField]
    private Rectangle spawnRange;
    [SerializeField]
    private GameObject itemBoxPrefab;
    private int spawnInterval;

    private static float WORLDTIME = 0;

    void Awake()
    {
        
    }

    void Update()
    {
        WORLDTIME += Time.deltaTime;
    }

    private void spawnItemBox(){
        GameObject clone = Instantiate(itemBoxPrefab, getRandomPos(), Quaternion.identity);
    }

    private Vector3 getRandomPos(){
        System.Random random = new System.Random();
        return new Vector3();
    }
}
