using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHpUIController : MonoBehaviour
{
    private MiniHpUIView miniHpUIView;

    private static Vector3 miniHpUIUpVector = new Vector3(0f, 1.5f, 0f);

    private void Awake()
    {
        miniHpUIView = GetComponentInChildren<MiniHpUIView>();
    }

    private void LateUpdate()
    {
        miniHpUIView.transform.position = Camera.main.WorldToScreenPoint(transform.position + miniHpUIUpVector);
    }

    public void SetHpBarHealth(float _currHp, float _maxHp)
    {
        miniHpUIView.SetHpBarFillAmount(_currHp / _maxHp);
    }
}
