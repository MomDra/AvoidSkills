using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected GameObject hpBarPrefab;

    protected PlayerStatus status;
    protected bool canAttack = true;
    protected float attackDelayTimer = 0f;

    protected Rigidbody rigid;

    protected GameObject hpBar;
}
