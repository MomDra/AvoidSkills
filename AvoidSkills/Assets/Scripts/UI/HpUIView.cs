using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIView : MonoBehaviour
{
    private static HpUIView instance;

    private Image hpBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            hpBar = GetComponentInChildren<Image>();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 이상 생성되었습니다. 하나를 삭제합니다.");
        }
    }

    public void SetHpBarFillAmount(float _fillAmount)
    {
        hpBar.fillAmount = _fillAmount;
    }
}
