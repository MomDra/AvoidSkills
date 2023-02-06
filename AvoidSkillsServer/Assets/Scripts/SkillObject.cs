using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    public static Dictionary<int, SkillObject> skillObjects = new Dictionary<int, SkillObject>();
    private static int nextSkillObjectId = 1;

    public int id;
    [HideInInspector]
    public int ownPlayerID;
    public SkillInfo skillInfo { get; private set; }

    [SerializeField]
    private bool destroyWhenCollision;
    [SerializeField]
    private bool isContinuous;


    bool isSpawnedByRed;


    private void Awake()
    {
        id = nextSkillObjectId;
        ++nextSkillObjectId;
        skillObjects.Add(id, this);
    }

    private void FixedUpdate()
    {
        ServerSend.SkillObjectPositionUpdate(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isContinuous) return;

        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            if (otherPlayer.id != ownPlayerID)
            {
                if (otherPlayer.IsRed != isSpawnedByRed)
                {
                    otherPlayer.TakeDamage(skillInfo.damage);
                    ServerSend.SKillObjectHit(this, transform.position);
                }
            }
        }
        if (destroyWhenCollision) Destory();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isContinuous) return;

        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            if (otherPlayer.id != ownPlayerID)
            {
                if (otherPlayer.IsRed != isSpawnedByRed)
                    otherPlayer.TakeDamage(skillInfo.damage);
            }
        }
    }

    public void Initialize(int _ownPlayerID, SkillInfo _skillInfo, bool _isSpawnedByRed)
    {
        ownPlayerID = _ownPlayerID;
        skillInfo = _skillInfo;
        isSpawnedByRed = _isSpawnedByRed;
        ServerSend.InstantiateSkillObject(this, ownPlayerID);
        StartCoroutine(DestroySelf());
    }

    private void Explode()
    {
        ServerSend.SkillObjectExploded(this);

        skillObjects.Remove(id);
        Destroy(gameObject);
    }

    private void Destory()
    {
        skillObjects.Remove(id);
        ServerSend.DestorySkillObject(this);
        Destroy(gameObject);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(skillInfo.duration);
        Destory();
    }

    public static void Clear()
    {
        foreach (SkillObject item in skillObjects.Values)
        {
            Destroy(item.gameObject);
        }

        skillObjects.Clear();
        nextSkillObjectId = 1;
    }
}


