using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public PlayerStatus playerStatus;
    public Vector3 playerPos;

    public Command NormalAttackCommand{ get;  set; }

    public Command UserCustomSkillCommand { get;  set; }

    private Command[] itemSkills;

    public void normalAttack(){
        this.NormalAttackCommand.cmd(playerStatus, playerPos);
    }
}
