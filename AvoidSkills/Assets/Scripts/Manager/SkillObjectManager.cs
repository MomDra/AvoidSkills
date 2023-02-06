using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectManager : MonoBehaviour
{
    public int id;
    public GameObject hitEffectPrefab;
    public GameObject explosionPrefab;

    public void Initialize(int _id)
    {
        id = _id;
    }

    public void Explode(Vector3 _position)
    {
        transform.position = _position;

        if (hitEffectPrefab != null) Destroy(Instantiate(hitEffectPrefab, transform.position, Quaternion.identity), 5f);
        if (explosionPrefab != null) Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 5f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
