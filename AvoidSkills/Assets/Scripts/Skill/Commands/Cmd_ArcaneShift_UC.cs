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

    public override void GeneratePreview(Transform player)
    {
        throw new System.NotImplementedException();
    }

    public override void previewUpdate(Transform player)
    {
        throw new System.NotImplementedException();
    }

    public override void DestroyPreview()
    {
        throw new System.NotImplementedException();
    }

    public override void run(Transform player, PlayerStatus status)
    {
        status.playerStop = true;
        player.transform.position += (MousePointer.Instance.MousePositionInWorld - player.position).normalized * skillInfo.range;
    }
}