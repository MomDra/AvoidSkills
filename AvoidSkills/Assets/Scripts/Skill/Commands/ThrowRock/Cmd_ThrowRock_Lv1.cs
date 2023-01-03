using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd_ThrowRock_Lv1 : SkillCommand
{
    private static Cmd_ThrowRock_Lv1 instance;

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

    public GameObject pr{ get; private set; }
    private LineRenderer lr;
    private Vector3[] points = new Vector3[2];

    public override void GeneratePreview(Transform player)
    {
        pr = Instantiate(skillInfo.previewPrefab);
        pr.SetActive(true);
        lr = pr.GetComponent<LineRenderer>();
    }

    public override void previewUpdate(Transform player)
    {
        points[0] = player.position;
        points[1] = MousePointer.Instance.MousePositionInWorld;
        lr.SetPositions(points);
    }

    public override void DestroyPreview()
    {
        Destroy(pr);
    }

    public override void run(Transform player, PlayerStatus status)
    {
        Vector3 dir = (MousePointer.Instance.MousePositionInWorld - player.position).normalized;
        GameObject ob = Instantiate(skillInfo.skillPrefab, player.position + dir + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(ob, 2f);
        Vector3 force = dir * skillInfo.speed;

        ob.GetComponent<Rigidbody>().velocity = force;
    }
}