using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ThrowRock_Lv3 : SkillCommand
{
    private static Cmd_ThrowRock_Lv3 instance;

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
        GameObject ob1 = Instantiate(skillInfo.skillPrefab, player.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        GameObject ob2 = Instantiate(skillInfo.skillPrefab, player.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        GameObject ob3 = Instantiate(skillInfo.skillPrefab, player.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        Destroy(ob1, 4f);
        Destroy(ob2, 4f);
        Destroy(ob3, 4f);
        Vector3 force = (MousePointer.Instance.MousePositionInWorld - player.position).normalized * skillInfo.projectileSpeed;

        ob1.GetComponent<Rigidbody>().velocity = force;
        ob2.GetComponent<Rigidbody>().velocity = force;
        ob3.GetComponent<Rigidbody>().velocity = force;
    }
}
