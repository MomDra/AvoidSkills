using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIController : MonoBehaviour
{
    private static SkillUIController instance;


    SkillUIView skillUIView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            skillUIView = FindObjectOfType<SkillUIView>();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 이상 생성되었습니다. 하나를 삭제합니다.");
        }
    }

    public void SetSkillImage(Sprite _skillImg, int _slotNum)
    {
        skillUIView.SetSkillImage(_skillImg, _slotNum);
    }

    public void RemoveSkillImage(int _slotNum){
        skillUIView.RemoveSkillImage(_slotNum);
    }

    public void SetCoolTimeGauge(float _currTime, float _maxTime, int _slotNum)
    {
        skillUIView.SetSkillFillAmount(_currTime / _maxTime, _slotNum);
    }
}
