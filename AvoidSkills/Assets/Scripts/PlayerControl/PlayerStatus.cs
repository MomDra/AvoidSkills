using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    STAND,
    STUN,
    SNARE,
    BLIND
}

public class PlayerStatus : MonoBehaviour
{
    [HideInInspector]
    public State state = State.STAND;

    public int hp;
    public int armor;

    [SerializeField]
    public float attackSpeed;
    [SerializeField]
    public float moveSpeed;
    
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool playerStop = false;
}


