using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_FireCircle_Lv1 : SkillCommand
{
    private static Cmd_FireCircle_Lv1 instance;

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
        Debug.Log("Fire");
        GameObject ob = Instantiate(skillInfo.skillPrefab, _mousePos + Vector3.down, Quaternion.identity);
        ob.transform.localScale = new Vector3(skillInfo.range, ob.transform.localScale.y , skillInfo.range);
        ob.GetComponent<Projectile>().Initialize(_player.id, 5f, skillInfo);
    }
}
