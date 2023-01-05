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

    [SerializeField]
    private Sprite crownImage;
    [SerializeField]
    private Sprite checkImage;

    int[] paneUserId;

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

            paneUserId = new int[4] { -1, -1, -1, -1 };
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    public void RemoveMember(int _userId)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (paneUserId[i] == _userId)
            {
                playerPanes[i].SetActive(false);
                paneUserId[i] = -1;

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
            if (paneUserId[i] == -1)
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
            if (paneUserId[i] == -1)
            {
                SetMember(_gameRoomUser, i);
                return;
            }
        }

        throw new System.Exception("Red User가 3명 이상입니다.");
    }

    private void SetMember(GameRoomUser _gameRoomUser, int _index)
    {
        paneUserId[_index] = _gameRoomUser.id;
        playerPanes[_index].SetActive(true);
        playerPanes[_index].GetComponentInChildren<TextMeshProUGUI>().text = _gameRoomUser.userName;
        Image _stateImage = playerPanes[_index].GetComponentsInChildren<Image>()[4];

        if (_gameRoomUser.isRoomKing)
        {
            _stateImage.sprite = crownImage;
            _stateImage.color = Color.white;
        }
        else
        {
            _stateImage.sprite = null;
            _stateImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void SetRoomKing(int _roomKingId)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (paneUserId[i] == _roomKingId)
            {
                Image _stateImage = playerPanes[i].GetComponentsInChildren<Image>()[4];
                _stateImage.sprite = crownImage;
                _stateImage.color = Color.white;
            }
        }
    }

    private void DisConnectToServer()
    {
        SceneManager.LoadScene(0);
        Client.Instance.Disconnect();
    }
}
