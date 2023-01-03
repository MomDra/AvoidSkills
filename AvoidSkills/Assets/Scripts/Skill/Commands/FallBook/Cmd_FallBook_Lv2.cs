using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_FallBook_Lv2 : SkillCommand
{
    private static Cmd_FallBook_Lv2 instance;

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
        GameObject ob = Instantiate(skillInfo.skillPrefab, MousePointer.Instance.MousePositionInWorld + Vector3.up * 7, Quaternion.identity);
        Destroy(ob, 1.5f);
        
        ob.GetComponent<Rigidbody>().velocity = Vector3.down * skillInfo.speed;

    }
}
