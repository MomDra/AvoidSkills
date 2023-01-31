using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Dictionary<int, Projectile> projectiles = new Dictionary<int, Projectile>();
    private static int nextProjectileId = 1;

    [HideInInspector]
    public int id;
    [HideInInspector]
    public int thrownByPlayer;
    private float destroyTime;
    [HideInInspector]
    public SkillCode skillCode;
    [HideInInspector]
    public SkillLevel skillLevel;

    [SerializeField]
    private bool destroyWhenCollision;


    private void Start()
    {
        id = nextProjectileId;
        ++nextProjectileId;
        projectiles.Add(id, this);

        ServerSend.SpawnProjectile(this, thrownByPlayer);

        StartCoroutine(DestroySelf());
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
            if (otherPlayer.id != thrownByPlayer)
            {
                otherPlayer.TakeDamage(10);
                if (destroyWhenCollision) Destory();
            }
        }
    }

    public void Initialize(int _thrownByPlayer, float _destroyTime, SkillCode _skillCode, SkillLevel _skillLevel)
    {
        thrownByPlayer = _thrownByPlayer;
        destroyTime = _destroyTime;
        skillCode = _skillCode;
        skillLevel = _skillLevel;
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
        projectiles.Clear();
        nextProjectileId = 1;
    }
}


