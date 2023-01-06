using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUIView : MonoBehaviour
{
    private static TimerUIView instance;
    public static TimerUIView Instance { get => instance; }

    private TextMeshProUGUI timerMinuteText;
    private TextMeshProUGUI timerSecondText;

    private int currentMinutes = 0;
    private int currentSeconds = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
            timerMinuteText = texts[0];
            timerSecondText = texts[2];
        }
        else if (instance != this)
        {
            Destroy(this);

            Debug.Log("객체가 2개 생성되었습니다. 객체를 삭제합니다.");
        }
    }

    private void Start() {
        StartCoroutine(TimerTextUpdate());
    }

    private IEnumerator TimerTextUpdate(){
        while(true){
            yield return new WaitForSeconds(1f);
            if(++currentSeconds == 60){
                ++currentMinutes;
                timerMinuteText.text = string.Format("{0:D2}", currentMinutes);
                currentSeconds = 0;
            }
            timerSecondText.text = string.Format("{0:D2}", currentSeconds);
        }
    }

}
