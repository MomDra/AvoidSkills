using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultUIView : MonoBehaviour
{
    private static ResultUIView instance;
    public static ResultUIView Instance { get => instance; }

    private GameObject resultPane;

    private Button okButton;

    private TextMeshProUGUI resultText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FindComponents();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void FindComponents(){
        resultPane = transform.GetChild(3).gameObject;
        resultText = resultText.GetComponentInChildren<TextMeshProUGUI>();
        okButton = resultPane.GetComponentInChildren<Button>();
    }

    private void AddButtonListeners(){
        okButton.onClick.AddListener(QuitGame);
    }

    public void OpenResultPane(bool _isRedTeamWin){
        resultPane.SetActive(true);
        if(_isRedTeamWin == MemberModel.Instance.myUser.isRed){
            resultText.text = "Victory!!";
        }else{
            resultText.tag = "Defeat..";
        }
        StartCoroutine(WaitForGameEnd());
    }

    private IEnumerator WaitForGameEnd(){
        yield return new WaitForSeconds(5f);
        QuitGame();
    }

    private void QuitGame(){
        SceneManager.LoadScene(1);
    }
}
