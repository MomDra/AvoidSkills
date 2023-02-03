using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Dictionary<int, Projectile> projectiles = new Dictionary<int, Projectile>();
    private static int nextProjectileId = 1;

    public int id;
    [HideInInspector]
    public int ownPlayerID;
    private float destroyTime;
    [HideInInspector]
    public SkillCode skillCode;
    [HideInInspector]
    public SkillLevel skillLevel;

    private SkillInfo skillInfo;

    [SerializeField]
    private bool destroyWhenCollision;


    private void Awake()
    {
        id = nextProjectileId;
        ++nextProjectileId;
        projectiles.Add(id, this);
        
    }

    private void FixedUpdate()
    {
        ServerSend.ProjectilePosition(this);
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

    public void Initialize(int _ownPlayerID, float _destroyTime, SkillInfo _skillInfo)
    {
        ownPlayerID = _ownPlayerID;
        destroyTime = (_destroyTime != 0) ? _destroyTime : 5f;
        skillInfo = _skillInfo;
        skillCode = skillInfo.skillCode;
        skillLevel = skillInfo.level;
        ServerSend.SpawnProjectile(this, ownPlayerID);
        StartCoroutine(DestroySelf());
    }

    private void Explode()
    {
        ServerSend.ProjectileExploded(this);

        projectiles.Remove(id);
        Destroy(gameObject);
    }

    private void Destory()
    {
        projectiles.Remove(id);
        ServerSend.DestoryProjectile(this);
        Destroy(gameObject);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destory();
    }

    public static void Clear()
    {
        foreach(KeyValuePair<int,Projectile> item in projectiles){
            Destroy(item.Value);
        }
        projectiles.Clear();
        nextProjectileId = 1;
    }
}


