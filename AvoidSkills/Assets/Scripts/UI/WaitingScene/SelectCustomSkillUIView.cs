using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCustomSkillUIView : MonoBehaviour
{
    private static SelectCustomSkillUIView instance;
    public static SelectCustomSkillUIView Instance { get => instance; }

    private Button selectCustomSkillButton;

    private GameObject customSkillListPane;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            selectCustomSkillButton = GetComponentInChildren<Button>();
            selectCustomSkillButton.onClick.AddListener(OnOffCustomSkillListUIView);
            customSkillListPane = selectCustomSkillButton.transform.GetChild(0).gameObject;
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void OnOffCustomSkillListUIView(){
        if(customSkillListPane.activeSelf) customSkillListPane.SetActive(false);
        else customSkillListPane.SetActive(true);
    }
}
