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
        GameObject ob = Instantiate(skillInfo.skillPrefab, MousePointer.Instance.MousePositionInWorld + Vector3.up * 7, Quaternion.identity);
        Destroy(ob, 1.5f);
        
        ob.GetComponent<Rigidbody>().velocity = Vector3.down * skillInfo.speed;
    }

}
