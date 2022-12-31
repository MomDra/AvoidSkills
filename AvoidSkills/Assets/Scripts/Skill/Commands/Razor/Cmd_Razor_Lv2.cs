using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_Razor_Lv2 : SkillCommand
{
    private static Cmd_Razor_Lv2 instance;

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
        GameObject ob = Instantiate(skillInfo.skillPrefab, player.position, Quaternion.identity);
        Destroy(ob, 2f);
        TrailRenderer tr = ob.GetComponentInChildren<TrailRenderer>();
        tr.startWidth = skillInfo.range;
        ob.GetComponent<Rigidbody>().velocity = (MousePointer.Instance.MousePositionInWorld - player.position).normalized * skillInfo.speed;
    }
}
