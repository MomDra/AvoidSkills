using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionUIView : MonoBehaviour
{
    private static OptionUIView instance;
    public static OptionUIView Instance { get => instance; }


    private GameObject currentPane;
    private GameObject optionPane;
    private GameObject reallyExitPane;

    private Button exitButton;
    private Button reallyExitButton;
    private Button reallyExitCancelButton;

    private Button applyButton;

    private IEnumerator iEmumerator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FindComponents();
            AddButtonListeners();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (currentPane != null)
            {
                currentPane.gameObject.SetActive(false);
                if(currentPane == reallyExitPane){
                    CloseReallyExitPane();
                }
            }
        }
    }

    private void FindComponents(){
        optionPane = transform.GetChild(0).gameObject;
        GameObject bottom_Bar = optionPane.transform.GetChild(3).gameObject;
        Button[] buttons = bottom_Bar.GetComponentsInChildren<Button>();
        exitButton = buttons[0];
        reallyExitPane = exitButton.transform.GetChild(1).gameObject;
        Button[] buttons2 = reallyExitPane.GetComponentsInChildren<Button>();
        reallyExitButton = buttons2[0];
        reallyExitCancelButton = buttons2[1];

        applyButton = buttons[1];
    }

    private void AddButtonListeners(){
        GetComponent<Button>().onClick.AddListener(OpenOptionPane);
        exitButton.onClick.AddListener(OpenReallyExitPane);
        reallyExitButton.onClick.AddListener(ExitGame);
        reallyExitCancelButton.onClick.AddListener(CloseReallyExitPane);
    }

    private void OpenOptionPane(){
        optionPane.gameObject.SetActive(true);
        currentPane = optionPane;
    }


    private void OpenReallyExitPane(){
        reallyExitPane.SetActive(true);
        currentPane = reallyExitPane;
        iEmumerator = WaitForEnableButtonCoroutine();
        StartCoroutine(iEmumerator);
    }

    private void CloseReallyExitPane(){
        reallyExitPane.SetActive(false);
        currentPane = optionPane;
        StopCoroutine(iEmumerator);
    }

    private void ApplyOption(){
        optionPane.gameObject.SetActive(false);
        currentPane = null;
    }

    private IEnumerator WaitForEnableButtonCoroutine(){
        reallyExitButton.interactable = false;
        for (int i = 5; i > 0;--i){
            reallyExitButton.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        reallyExitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit Game";
        reallyExitButton.interactable = true;
    }

    private void ExitGame(){
        SceneManager.LoadScene(0);
        MemberModel.Instance.Clear();
        Client.Instance.Disconnect();
    }
}
