using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public int itemCount = 0;
    public MeshRenderer model;

    private HpUIController hpUIController;
    private bool isLocalPlayer;

    private void Awake()
    {
        hpUIController = GetComponent<HpUIController>();
    }

    public void Initialize(int _id, string _username, bool _isLocalPlayer)
    {
        id = _id;
        username = _username;
        health = maxHealth;
        isLocalPlayer = _isLocalPlayer;
    }

    public void SetHealth(float _health)
    {
        health = _health;

        Debug.Log($"health: {_health}, max: {maxHealth}");

        if (isLocalPlayer) hpUIController.SetHpBarHealth(health, maxHealth);

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }
}
