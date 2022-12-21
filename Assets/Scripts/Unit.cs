using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected enum State
    {
        STAND,
        STUN,
        SNARE,
        BLIND
    }
    [SerializeField]
    protected float attackSpeed;

    protected bool canAttack = true;
    protected float attackDelayTimer = 0f;


    protected Rigidbody rigid;

}
