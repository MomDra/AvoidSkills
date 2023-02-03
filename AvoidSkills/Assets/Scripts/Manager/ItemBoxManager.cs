using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxManager : MonoBehaviour
{
    public int id;
    public int level;
    public GameObject explosionPrefab;

    public void Initialize(int _id)
    {
        id = _id;
        level = 0;
    }

    public void LevelUpdate(int amount = 1){
        level += amount;
        ColorUpdate();
    }

    public void ColorUpdate(){
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color _color = Color.white;
        switch(level){
            case 1:
                _color = Color.yellow;
                break;
            case 2:
                _color = Color.red;
                break;
        }
        meshRenderer.material.color = _color;
    }

    public void Destory()
    {
        Destroy(gameObject);
    }

}
