using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxManager : MonoBehaviour
{
    public int id;
    public int level;
    public GameObject itemBoxPrefab;
    public GameObject explosionPrefab;

    public void Initialize(int _id)
    {
        id = _id;
    }

    public void LevelUpdate(int amount = 1){
        level += amount;
        ColorUpdate();
    }

    private void ColorUpdate(){

    }

    public void Destory()
    {
        Destroy(gameObject);
    }

}
