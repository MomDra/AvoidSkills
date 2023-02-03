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


    public override void cmd(Player _player, PlayerStatus _status, Vector3 _mousePos)
    {
        Vector3 dir = (_mousePos - _player.transform.position).normalized;
        GameObject ob1 = Instantiate(skillInfo.skillPrefab, _player.transform.position + dir + new Vector3(0, 0.5f, 0), Quaternion.identity);
        GameObject ob2 = Instantiate(skillInfo.skillPrefab, _player.transform.position + dir + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Vector3 velocity = dir * skillInfo.projectileSpeed;

        ob1.GetComponent<Rigidbody>().velocity = velocity;
        ob1.GetComponent<Projectile>().Initialize(_player.id, 2f, skillInfo);
        ob2.GetComponent<Rigidbody>().velocity = velocity;
        ob2.GetComponent<Projectile>().Initialize(_player.id, 2f, skillInfo);
    }
}
