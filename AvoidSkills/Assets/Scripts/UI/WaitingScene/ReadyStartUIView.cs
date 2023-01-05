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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            readyStartButton = GetComponentInChildren<Button>();

            readyStartButton.onClick.AddListener(ReadyStartGame);
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    public void SetReadyStartButton(bool _isRoomKing)
    {
        if (_isRoomKing)
        {
            readyStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
        else
        {
            readyStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ready";
        }
    }


    private void ReadyStartGame()
    {
        // id와 reay버튼을 눌렀음 보냄

        // ready 버튼 색깔 ㅠㅏ란색으로 바꿈
        // check 표시 하는거 이런거

        // ClientSend.ReadyStartButton();
    }
}
