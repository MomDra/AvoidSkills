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
        GameObject ob = Instantiate(skillInfo.skillPrefab, player.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        Destroy(ob, 4f);
        Vector3 force = (MousePointer.Instance.MousePositionInWorld - player.position).normalized * skillInfo.projectileSpeed;

        ob.GetComponent<Rigidbody>().velocity = force;
    }
}