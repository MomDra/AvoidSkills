using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Dictionary<int, Projectile> projectiles = new Dictionary<int, Projectile>();
    private static int nextProjectileId = 1;

    public int id;
    public Rigidbody rigid;
    public int thrownByPlayer;
    public Vector3 initialForce;


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
            }

            Destory();
        }
    }

    public void Initialize(int _thrownByPlayer)
    {
        thrownByPlayer = _thrownByPlayer;
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
        yield return new WaitForSeconds(0.8f);

        Destory();
    }
}


