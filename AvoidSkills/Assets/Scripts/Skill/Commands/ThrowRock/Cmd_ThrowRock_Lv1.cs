using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ThrowRock_Lv1 : SkillCommand
{
    private static Cmd_ThrowRock_Lv1 instance;

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
        GameObject ob = Instantiate(skillInfo.skillPrefab, player.position + dir + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(ob, 2f);
        Vector3 force = dir * skillInfo.projectileSpeed;

        ob.GetComponent<Rigidbody>().velocity = force;
    }
}