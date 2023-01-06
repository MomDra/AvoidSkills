using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIView : MonoBehaviour
{
    private static ScoreUIView instance;
    public static ScoreUIView Instance { get => instance; }

    private TextMeshProUGUI blueTeamScoreText;
    private TextMeshProUGUI redTeamScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
            blueTeamScoreText = texts[0];
            redTeamScoreText = texts[1];
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    public void ScoreTextUpdate(int _blueTeamScore, int _redTeamScore){
        blueTeamScoreText.text = _blueTeamScore.ToString();
        redTeamScoreText.text = _redTeamScore.ToString();
    }
    


}
