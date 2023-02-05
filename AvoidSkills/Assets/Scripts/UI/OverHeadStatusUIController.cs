using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadStatusUIController : MonoBehaviour
{
    private OverHeadStatusUIView overHeadStatusUIView;

    private static Vector3 offset = new Vector3(0f, 1.5f, 0f);

    private void Awake()
    {
        overHeadStatusUIView = GetComponentInChildren<OverHeadStatusUIView>();
    }

    private void LateUpdate()
    {
        overHeadStatusUIView.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    public void SetUsername(string _username, bool _isRed)
    {
        overHeadStatusUIView.SetUsername(_username, _isRed);
    }

    public void SetHpBarHealth(float _currHp, float _maxHp)
    {
        overHeadStatusUIView.SetHpBarFillAmount(_currHp / _maxHp);
    }
}
