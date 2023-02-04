using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyStartUIView : MonoBehaviour
{
    private static ReadyStartUIView instance;
    public static ReadyStartUIView Instance { get => instance; }

    private Button readyStartButton;
    private Button testLabStartButton;

    private bool isRoomKing;
    private bool isReady;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Button[] buttons = GetComponentsInChildren<Button>();
            readyStartButton = buttons[0];
            testLabStartButton = buttons[1];

            readyStartButton.onClick.AddListener(PressedReadyStartButton);
            testLabStartButton.onClick.AddListener(PressedStartTestLabButton);
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void Start()
    {
        if (MemberModel.Instance.myUser != null)
        {
            ReadyStartTextUpdate(MemberModel.Instance.myUser.isRoomKing);
        }
    }

    public void ReadyStartTextUpdate(bool _isRoomKing)
    {
        if (_isRoomKing)
        {
            readyStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
            isRoomKing = true;
        }
        else
        {
            readyStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ready";
            isRoomKing = false;
        }
    }

    public void TestLabStartButtonInterableUpdate(bool _isRoomKing){
        if(_isRoomKing) testLabStartButton.interactable = true;
        else testLabStartButton.interactable = false;
    }


    private void PressedReadyStartButton()
    {

        if (isRoomKing)
        {
            StartCoroutine(InteractableFalse(2f));
            ClientSend.StartButton();
        }
        else
        {
            isReady = !isReady;
            StartCoroutine(InteractableFalse(0.1f));
            ClientSend.ReadyButton(isReady);
        }
    }

    private void PressedStartTestLabButton(){
        ClientSend.TestLabStartButton();
    }

    private IEnumerator InteractableFalse(float time)
    {
        readyStartButton.interactable = false;
        yield return new WaitForSeconds(time);
        readyStartButton.interactable = true;
    }
}
