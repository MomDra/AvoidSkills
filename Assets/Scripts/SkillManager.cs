using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public GameObject player;
    public PlayerStatus status;
    public Vector3 playerPos;

    public Command NormalAttackCommand{ get;  set; }

    public Command UserCustomSkillCommand { get;  set; }

    private Command[] itemSkills;

    public void normalAttack(){
        this.NormalAttackCommand.cmd(player, status, playerPos);
    }

    public void userCustomSkill(){
        this.UserCustomSkillCommand.cmd(player, status, playerPos);
    }
}
