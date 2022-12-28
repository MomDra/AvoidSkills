using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_DropRock_Lv1 : SkillCommand
{
    private static Cmd_DropRock_Lv1 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    public override void cmd(Transform player, PlayerStatus status)
    {
        Debug.Log("DropRock1");
    }
}