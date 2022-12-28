using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerStatus playerStatus;

    private SkillCommand normalAttackCommand;

    private SkillCommand userCustomSkillCommand;

    private SkillCommand[] itemSkillCommands;

    private CoolDownTimer currCoolDown_NormalAttack = new CoolDownTimer();
    private CoolDownTimer currCoolDown_UserCustomSkill= new CoolDownTimer();
    private CoolDownTimer currCoolDown_ItemSkill1= new CoolDownTimer();
    private CoolDownTimer currCoolDown_ItemSkill2= new CoolDownTimer();
    private CoolDownTimer currCoolDown_ItemSkill3= new CoolDownTimer();

    private void Awake()
    {
        this.playerTransform = this.transform;
        this.playerStatus = GetComponent<PlayerStatus>();
        normalAttackCommand = SkillDB.Instance.GetSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1);
        userCustomSkillCommand = SkillDB.Instance.GetSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1);

        itemSkillCommands = new SkillCommand[3];
    }

    private IEnumerator CoolDownCoroutine(CoolDownTimer timer){
        while(timer.currTime > 0){
            yield return new WaitForSeconds(0.1f);
            timer.tik();
        }
        timer.reset();
    }

    private bool CheckCoolDown(SkillInfo skillInfo, CoolDownTimer currCoolDown){
        if(currCoolDown.currTime==0){
            currCoolDown.set(skillInfo.coolDownTime);
            StartCoroutine(CoolDownCoroutine(currCoolDown));
            return true;
        }
        Debug.Log(skillInfo.skillName + " 이(가) 아직 준비되지 않았습니다!");
        return false;
    }

    public void NormalAttack()
    {
        if(CheckCoolDown(normalAttackCommand.SkillInfo, currCoolDown_NormalAttack))
            normalAttackCommand.cmd(playerTransform, playerStatus);
    }

    public void UserCustomSkill()
    {
        if(CheckCoolDown(userCustomSkillCommand.SkillInfo, currCoolDown_UserCustomSkill)) 
            userCustomSkillCommand.cmd(playerTransform, playerStatus);
    }

    public void ItemSkill1()
    {
        if (itemSkillCommands[0] != null)
        {
            if(CheckCoolDown(itemSkillCommands[0].SkillInfo, currCoolDown_ItemSkill1)) 
            itemSkillCommands[0].cmd(playerTransform, playerStatus);
        }
        else Debug.Log("1번 스킬은 비어있습니다!");
    }

    public void ItemSkill2()
    {
        if (itemSkillCommands[1] != null)
        {
            if(CheckCoolDown(itemSkillCommands[1].SkillInfo, currCoolDown_ItemSkill2)) 
            itemSkillCommands[1].cmd(playerTransform, playerStatus);
        }
        else Debug.Log("2번 스킬은 비어있습니다!");
    }

    public void ItemSkill3()
    {
        if (itemSkillCommands[2] != null)
        {
            if(CheckCoolDown(itemSkillCommands[3].SkillInfo, currCoolDown_ItemSkill3)) 
            itemSkillCommands[3].cmd(playerTransform, playerStatus);
        }
        else Debug.Log("3번 스킬은 비어있습니다!");
    }
}
