using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int hp;
    public int mp;
    public int armor;

    public float attackSpeed;
    public float moveSpeed;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool playerStop = false;
}


