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
    private Material material;

    private HpUIController hpUIController;
    private OverHeadStatusUIController overHeadStatusUIController;
    private bool isLocalPlayer;

    private void Awake()
    {
        hpUIController = GetComponent<HpUIController>();
        overHeadStatusUIController = GetComponent<OverHeadStatusUIController>();
        material = GetComponentInChildren<Renderer>().material;
    }

    public void Initialize(int _id, string _username, bool _isLocalPlayer, bool _isRed)
    {
        id = _id;
        username = _username;
        overHeadStatusUIController.SetUsername(username, _isRed);
        health = maxHealth;
        isLocalPlayer = _isLocalPlayer;
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (isLocalPlayer) hpUIController.SetHpBarHealth(health, maxHealth);
        overHeadStatusUIController.SetHpBarHealth(health, maxHealth);

        StartCoroutine(ChangePlayerColor(Color.red, 0.1f));

        if (health <= 0f)
        {
            Die();
        }
    }

    private IEnumerator ChangePlayerColor(Color _color, float _time){
        Color currentColor = material.color;
        material.color = _color;
        yield return new WaitForSeconds(_time);
        material.color = currentColor;
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
