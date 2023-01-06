using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUIView : MonoBehaviour
{
    private static OptionUIView instance;
    public static OptionUIView Instance { get => instance; }

    private GameObject settingPane;

    private Button exitButton;
    private Button applyButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject bottom_Bar = transform.GetChild(2).gameObject;
            Button[] buttons = bottom_Bar.GetComponentsInChildren<Button>();
            exitButton = buttons[0];
            exitButton.onClick.AddListener(DisconnectCurrentGame);
            applyButton = buttons[1];
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void DisconnectCurrentGame(){
        Debug.Log("Disconnect");
        //SceneManager.LoadScene(0);
    }
}
