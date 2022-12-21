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
        
        if (Input.GetMouseButtonDown(0) && canAttack){
            canAttack = false;
            skillManager.normalAttack();
        }
        if(Input.GetMouseButtonDown(1)){
            if(status.isMoving) StartCoroutine(MovePosUpdateCoroutine());
            else StartCoroutine(MoveCoroutine());
        }

        if(Input.GetKeyDown(KeyCode.F)){
            skillManager.userCustomSkill();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            status.playerStop = true;
        }else if(Input.GetKeyUp(KeyCode.S)){
            status.playerStop = false;
        }
    }

    private IEnumerator MoveCoroutine(){ // Move by Mouse
        status.isMoving = true;
        Vector3 orgPos = transform.position;
        Vector3 toPos = MousePointer.Instance.MousePositionInWorld;
        float limitDistance = Vector3.Distance(orgPos, toPos);
        while(orgPos != toPos){
            transform.position = Vector3.MoveTowards(transform.position, toPos, status.moveSpeed * Time.deltaTime);
            if(status.playerStop) break;
            if (Vector3.Distance(orgPos, transform.position) >= limitDistance){
                transform.position = toPos;
                break;
            }
            yield return null;
        }
        status.isMoving = false;
        status.playerStop = false;
    }

    private IEnumerator MovePosUpdateCoroutine(){
        status.playerStop = true;
        yield return new WaitForSeconds(0.01f);
        status.playerStop = false;
        StartCoroutine(MoveCoroutine());
    }
}
