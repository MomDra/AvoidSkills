using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRockCommand3 : SkillCommand
{
    private static DropRockCommand3 instance;

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
        Debug.Log("DropRock3");
    }
}
