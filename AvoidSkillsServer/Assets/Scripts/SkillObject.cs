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
    public SkillInfo skillInfo{ get; private set; }

    [SerializeField]
    private bool destroyWhenCollision;


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
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            if (otherPlayer.id != ownPlayerID)
            {
                otherPlayer.TakeDamage(skillInfo.damage);
            }
        }
        if (destroyWhenCollision) Destory();
    }

    public void Initialize(int _ownPlayerID, SkillInfo _skillInfo)
    {
        ownPlayerID = _ownPlayerID;
        skillInfo = _skillInfo;
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
        foreach(KeyValuePair<int,SkillObject> item in skillObjects){
            Destroy(item.Value);
        }
        skillObjects.Clear();
        nextSkillObjectId = 1;
    }
}


