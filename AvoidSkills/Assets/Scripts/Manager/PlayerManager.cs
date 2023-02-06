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

    private Color initialColor;
    private bool canChangeColor;

    private HpUIController hpUIController;
    private OverHeadStatusUIController overHeadStatusUIController;
    private bool isLocalPlayer;

    private void Awake()
    {
        hpUIController = GetComponent<HpUIController>();
        overHeadStatusUIController = GetComponent<OverHeadStatusUIController>();
        material = GetComponentInChildren<Renderer>().material;

        canChangeColor = true;
        initialColor = material.color;
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

        if (canChangeColor)
            StartCoroutine(ChangePlayerColor(Color.red, 0.1f));

        if (health <= 0f)
        {
            Die();
        }
    }

    private IEnumerator ChangePlayerColor(Color _color, float _time)
    {
        canChangeColor = false;
        material.color = _color;
        yield return new WaitForSeconds(_time);
        material.color = initialColor;
        canChangeColor = true;
    }

    public void Die()
    {
        model.material.color = initialColor;
        model.enabled = false;
    }

    public void Respawn()
    {
        model.material.color = initialColor;
        model.enabled = true;
        SetHealth(maxHealth);
    }
}
