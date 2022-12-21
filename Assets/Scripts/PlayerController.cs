using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private State state = State.STAND;

    private SkillManager skillManager = new SkillManager();

    [HideInInspector]
    public PlayerStatus status;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        skillManager.player = this.gameObject;
        skillManager.NormalAttackCommand = new NormalArrowCommand();
        skillManager.UserCustomSkillCommand = new ArcaneShiftCommand();

        attackDelayTimer = 1f / attackSpeed;
    }

    private void Update() 
    {
        skillManager.status = status;
        skillManager.playerPos = transform.position;
        
        if(!canAttack){
            attackDelayTimer -= Time.deltaTime;
            if(attackDelayTimer <=0){
                canAttack = true;
                attackDelayTimer = 1f / attackSpeed;
            }
        }
        
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            skillManager.normalAttack();
        }
        if(Input.GetKeyDown(KeyCode.F)){
            skillManager.userCustomSkill();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(moveDirX, 0f, moveDirZ);
        Vector3 velocity = moveVec.normalized * status.moveSpeed;

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
