using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_FireCircle_Lv3 : SkillCommand
{
    private static Cmd_FireCircle_Lv3 instance;

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
        GameObject ob = Instantiate(skillInfo.skillPrefab, MousePointer.Instance.MousePositionInWorld + Vector3.down, Quaternion.identity);
        ob.transform.localScale = new Vector3(skillInfo.range, ob.transform.localScale.y , skillInfo.range);
        Destroy(ob, 6f);

    }
}
