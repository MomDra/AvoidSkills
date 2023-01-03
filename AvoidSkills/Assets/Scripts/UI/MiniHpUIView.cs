using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHpUIView : MonoBehaviour
{
    private Image hpBar;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Image>();
    }

    public void SetHpBarFillAmount(float _fillAmount)
    {
        hpBar.fillAmount = _fillAmount;
    }
}
