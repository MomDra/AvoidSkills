using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUIController : MonoBehaviour
{
    private static HpUIController instance;

    private HpUIView hpUIView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            hpUIView = FindObjectOfType<HpUIView>();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 이상 생성되었습니다. 하나를 삭제합니다.");
        }
    }

    public void SetHpBarHealth(float _currHp, float _maxHp)
    {
        hpUIView.SetHpBarFillAmount(_currHp / _maxHp);
    }
}
