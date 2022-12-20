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
    protected int moveSpeed;
    [SerializeField]
    protected int attackSpeed;
    [SerializeField]
    protected int arrowSpeed;

    protected bool isAttack;

    protected Rigidbody rigid;

}
