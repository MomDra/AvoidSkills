using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_NormalArrow_NA : SkillCommand
{
    private static Cmd_NormalArrow_NA instance;

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
        Vector3 arrowPos = player.position + (MousePointer.Instance.MousePositionInWorld - player.position).normalized;
        Vector3 velocity = (MousePointer.Instance.MousePositionInWorld - arrowPos).normalized * skillInfo.projectileSpeed;

        GameObject clone = Instantiate(skillInfo.skillPrefab, arrowPos, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = velocity;
    }
}