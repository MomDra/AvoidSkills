using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MemberUIView : MonoBehaviour
{
    private static MemberUIView instance;
    public static MemberUIView Instance { get => instance; }

    [SerializeField]
    private GameObject[] playerPanes;
    int[] isPaneUsed;

    [SerializeField]
    private Button exitButton;

    private int numBlueUser;
    private int numRedUser;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            exitButton.onClick.AddListener(DisConnectToServer);

            isPaneUsed = new int[4] { -1, -1, -1, -1 };
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void DisableAllUI()
    {
        foreach (GameObject playerPane in playerPanes)
        {
            playerPane.SetActive(false);
        }
    }

    public void RemoveMember(int _userId)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (isPaneUsed[i] == _userId)
            {
                playerPanes[i].SetActive(false);
                isPaneUsed[i] = -1;

                return;
            }
        }

        throw new System.Exception($"{_userId}가 존재하지 않습니다");
    }

    public void AddMember(GameRoomUser gameRoomUsers)
    {
        if (gameRoomUsers.isRed)
        {
            SetRedMember(gameRoomUsers, numBlueUser++);
        }
        else
        {
            SetBlueMember(gameRoomUsers, numBlueUser++);
        }
    }

    private void SetBlueMember(GameRoomUser _gameRoomUser, int _num)
    {
        for (int i = 0; i < 2; ++i)
        {
            if (isPaneUsed[i] == -1)
            {
                SetMember(_gameRoomUser, i);
                return;
            }
        }

        throw new System.Exception("Blue User가 3명 이상입니다.");
    }

    private void SetRedMember(GameRoomUser _gameRoomUser, int _num)
    {
        for (int i = 2; i < 4; ++i)
        {
            if (isPaneUsed[i] == -1)
            {
                SetMember(_gameRoomUser, i);
                return;
            }
        }

        throw new System.Exception("Red User가 3명 이상입니다.");
    }

    private void SetMember(GameRoomUser _gameRoomUser, int _index)
    {
        isPaneUsed[_index] = _gameRoomUser.id;
        playerPanes[_index].SetActive(true);
        playerPanes[_index].GetComponentInChildren<TextMeshProUGUI>().text = _gameRoomUser.userName;
    }

    private void DisConnectToServer()
    {
        SceneManager.LoadScene(0);
        Client.Instance.Disconnect();
    }
}
