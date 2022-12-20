using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum State{
        STAND,
        STUN,
        SNARE,
        BLIND
    }

    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int attackSpeed;
    [SerializeField]
    protected int arrowSpeed;
    
    protected bool isAttack;

    protected Rigidbody rigid;

    void Start(){
        rigid = GetComponent<Rigidbody>();
    }

}
