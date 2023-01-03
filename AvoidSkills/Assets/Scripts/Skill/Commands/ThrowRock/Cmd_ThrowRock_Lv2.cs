using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ThrowRock_Lv2 : SkillCommand
{
    private static Cmd_ThrowRock_Lv2 instance;

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
        Vector3 dir = (MousePointer.Instance.MousePositionInWorld - player.position).normalized;
        GameObject ob1 = Instantiate(skillInfo.skillPrefab, player.position + dir + new Vector3(0,0.5f,0), Quaternion.identity);
        GameObject ob2 = Instantiate(skillInfo.skillPrefab, player.position + dir + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(ob1, 2f);
        Destroy(ob2, 2f);

        Vector3 force = dir * skillInfo.speed;

        ob1.GetComponent<Rigidbody>().velocity = force;
        ob2.GetComponent<Rigidbody>().velocity = force;
        
    }
}