using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_FallBook_Lv1 : SkillCommand
{
    private static Cmd_FallBook_Lv1 instance;

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
        GameObject ob = Instantiate(skillInfo.skillPrefab, _mousePos + Vector3.up * 7, Quaternion.identity);
        ob.GetComponent<Rigidbody>().velocity = Vector3.down * skillInfo.projectileSpeed;
        ob.GetComponent<Projectile>().Initialize(_player.id, 1.5f, skillInfo);
    }
}
