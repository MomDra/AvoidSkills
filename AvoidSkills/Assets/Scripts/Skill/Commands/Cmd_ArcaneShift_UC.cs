using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ArcaneShift_UC : SkillCommand
{
    private static Cmd_ArcaneShift_UC instance;

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
        status.playerStop = true;
        player.transform.position += (MousePointer.Instance.MousePositionInWorld - player.position).normalized * skillInfo.range;
    }
}