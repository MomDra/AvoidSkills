using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffSettingUIView : MonoBehaviour
{
    private static OnOffSettingUIView instance;
    public static OnOffSettingUIView Instance { get => instance; }

    private GameObject settingPane;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            settingPane = transform.GetChild(0).gameObject;
            GetComponent<Button>().onClick.AddListener(OnOffUIView);
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            settingPane.gameObject.SetActive(false);
        }
    }

    private void OnOffUIView(){
        if(settingPane.activeSelf) settingPane.gameObject.SetActive(false);
        else settingPane.gameObject.SetActive(true);
    }


}
