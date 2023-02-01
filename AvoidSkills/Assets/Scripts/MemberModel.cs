using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberModel : MonoBehaviour
{
    private static MemberModel instance;
    public static MemberModel Instance { get => instance; }

    private Dictionary<int, GameUser> memberDic;

    public GameUser myUser { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            memberDic = new Dictionary<int, GameUser>();
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    public void AddMember(GameUser gameUsers)
    {
        if (gameUsers.id == Client.Instance.MyId)
        {
            myUser = gameUsers;
        }
        memberDic.Add(gameUsers.id, gameUsers);
        MemberUIView.Instance.AddMember(gameUsers);
    }

    public void AddMemberModelOnly(GameUser gameUsers)
    {
        if (gameUsers.id == Client.Instance.MyId)
        {
            myUser = gameUsers;
        }
        memberDic.Add(gameUsers.id, gameUsers);
    }

    public void RemoveMember(int _userId)
    {
        memberDic.Remove(_userId);
        MemberUIView.Instance.RemoveMember(_userId);
    }

    public void RemoveMemberModelOnly(int _userId)
    {
        memberDic.Remove(_userId);
    }

    public void SetRoomKing(int _userId)
    {
        memberDic[_userId].isRoomKing = true;
        MemberUIView.Instance.CrownImageUpdate(_userId);
        ReadyStartUIView.Instance.ReadyStartTextUpdate(myUser.id == _userId);
    }

    public void SetRoomKingModelOnly(int _userId)
    {
        memberDic[_userId].isRoomKing = true;
    }

    public void SetReady(int _userId, bool _isReady)
    {
        memberDic[_userId].isReady = true;
        MemberUIView.Instance.CheckImageUpdate(_userId, _isReady);
    }

    public void LoadMemberUI()
    {
        foreach (GameUser _user in memberDic.Values)
        {
            MemberUIView.Instance.AddMember(_user);
        }
    }

    public void Clear()
    {
        memberDic.Clear();
        myUser = null;
    }
}
