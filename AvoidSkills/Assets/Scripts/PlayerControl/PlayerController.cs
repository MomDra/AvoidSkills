using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Unit
{
    private SkillManager skillManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        skillManager = GetComponent<SkillManager>();

        hpBar.SetActive(true);
    }

    private void Update()
    {
        // Action 
        if (Input.GetMouseButtonDown(0))
        {
            skillManager.NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillManager.UserCustomSkill();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            skillManager.ItemSkill1();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            skillManager.ItemSkill2();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            skillManager.ItemSkill3();
        }

        // Move
        if (Input.GetMouseButtonDown(1))
        {
            if (status.isMoving) StartCoroutine(MovePosUpdateCoroutine());
            else StartCoroutine(MoveCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            status.playerStop = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            status.playerStop = false;
        }
    }

    private void LateUpdate()
    {
        
        hpBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
    }


    private IEnumerator MoveCoroutine()
    { // Move by Mouse
        status.isMoving = true;
        Vector3 orgPos = transform.position;
        Vector3 toPos = MousePointer.Instance.MousePositionInWorld;
        float limitDistance = Vector3.Distance(orgPos, toPos);
        while (orgPos != toPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, toPos, status.moveSpeed * Time.deltaTime);
            if (status.playerStop) break;
            if (Vector3.Distance(orgPos, transform.position) >= limitDistance)
            {
                transform.position = toPos;
                break;
            }
            yield return null;
        }
        status.isMoving = false;
        status.playerStop = false;
    }

    private IEnumerator MovePosUpdateCoroutine()
    {
        status.playerStop = true;
        yield return new WaitForSeconds(0.01f);
        status.playerStop = false;
        StartCoroutine(MoveCoroutine());
    }

    public void Damage(int amount){
        if(status.currHP - amount > 0){
            status.currHP -= amount;
        }else{
            Dead();
        }
    }

    private void Dead(){
        Debug.Log("Player Dead");
        this.gameObject.SetActive(false);
    }
    

}
