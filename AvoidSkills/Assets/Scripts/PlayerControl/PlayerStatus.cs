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
    public State state = State.STAND;

    public int maxHP;
    [HideInInspector]
    public int currHP;
    public int armor;
    public float moveSpeed;
    public bool isMoving = false;
    public bool playerStop = false;
}


