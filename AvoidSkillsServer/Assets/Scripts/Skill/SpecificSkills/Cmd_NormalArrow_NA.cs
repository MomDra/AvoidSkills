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

    public override Projectile cmd(Transform _player, PlayerStatus _status, Vector3 _mousePos)
    {
        Vector3 arrowPos = _player.position + (_mousePos - _player.position).normalized;
        Vector3 velocity = (_mousePos - arrowPos).normalized * skillInfo.projectileSpeed;

        GameObject clone = Instantiate(skillInfo.skillPrefab, arrowPos, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = velocity;

        return clone.GetComponent<Projectile>();
    }
}
