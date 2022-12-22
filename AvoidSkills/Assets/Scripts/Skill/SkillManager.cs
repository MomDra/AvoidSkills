using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private Transform playerTransform;
    private PlayerStatus playerStatus;

    private SkillCommand normalAttackCommand;

    private SkillCommand userCustomSkillCommand;

    private SkillCommand[] itemSkills;

    public SkillManager(Transform player, PlayerStatus status)
    {
        this.playerTransform = player;
        this.playerStatus = status;
        normalAttackCommand = SkillDB.Instance.GetSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1);
        userCustomSkillCommand = SkillDB.Instance.GetSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1);

        itemSkills = new SkillCommand[3];
    }

    public void NormalAttack()
    {
        normalAttackCommand.cmd(playerTransform, playerStatus);
    }

    public void UserCustomSkill()
    {
        userCustomSkillCommand.cmd(playerTransform, playerStatus);
    }

    public void ItemSkill1()
    {
        if (itemSkills[0] != null)
        {
            itemSkills[0].cmd(playerTransform, playerStatus);
        }
    }

    public void ItemSkill2()
    {
        if (itemSkills[1] != null)
        {
            itemSkills[1].cmd(playerTransform, playerStatus);
        }
    }

    public void ItemSkill3()
    {
        if (itemSkills[2] != null)
        {
            itemSkills[2].cmd(playerTransform, playerStatus);
        }
    }
}
