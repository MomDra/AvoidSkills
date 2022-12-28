using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillUIManager : MonoBehaviour
{
    private SkillManager skillManager;

    [SerializeField]
    private GameObject[] slotPrefabs;

    private Slot[] slots;

    private void Awake() {
        skillManager = GetComponent<SkillManager>();
        slots = new Slot[5];
        InitializeSkillUI();
    }

    private void InitializeSkillUI(){
        for (int i = 0; i < 5;++i){
            slots[i] = new Slot();
            slots[i].itemImage = slotPrefabs[i].transform.GetChild(0).GetComponent<Image>();
            slots[i].skillGauge = slotPrefabs[i].transform.GetChild(1).GetComponent<Image>();
            if(skillManager.skillComands[i]!=null){
                slots[i].itemImage.sprite = skillManager.skillComands[i].SkillInfo.skillImage;
            }
        }
    }

    public IEnumerator CoolDownGaugeUpdateCoroutine(int i){
        float currCoolDownTime = skillManager.currCoolDowns[i].currTime;
        Color gaugeColor = slots[i].skillGauge.color;

        while (currCoolDownTime >= 0)
        {
            currCoolDownTime = skillManager.currCoolDowns[i].currTime;
            slots[i].skillGauge.fillAmount = currCoolDownTime / skillManager.skillComands[i].SkillInfo.coolDownTime;
            gaugeColor.a = (currCoolDownTime / skillManager.skillComands[i].SkillInfo.coolDownTime) * 0.7f;
            slots[i].skillGauge.color = gaugeColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    
}
