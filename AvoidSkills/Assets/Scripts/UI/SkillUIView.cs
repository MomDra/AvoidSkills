using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIView : MonoBehaviour
{
    private static SkillUIView instance;

    [SerializeField]
    Slot[] slots;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 이상 생성되었습니다. 하나를 삭제합니다.");
        }
    }

    public void SetSkillImage(Sprite _skillImg, int _slotNum)
    {
        if (_skillImg == null) Debug.Log("hahahahaha");

        slots[_slotNum].itemImage.sprite = _skillImg;
        slots[_slotNum].skillGauge.fillAmount = 0f;
    }

    public void RemoveSkillImage(int _slotNum){
        slots[_slotNum].itemImage.sprite = null;
    }

    public void SetSkillFillAmount(float _fillAmount, int _slotNum)
    {
        slots[_slotNum].skillGauge.fillAmount = _fillAmount;
    }
}
