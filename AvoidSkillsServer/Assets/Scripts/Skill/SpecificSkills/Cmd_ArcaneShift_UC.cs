using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ArcaneShift_UC : SkillCommand
{
    private static Cmd_ArcaneShift_UC instance;

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
        _player.transform.position += (_mousePos - _player.transform.position).normalized * skillInfo.range;
        _player.GetComponent<Player>().SetTargetPos(_player.transform.position);
        ServerSend.PlayerPosition(_player);
    }
}
