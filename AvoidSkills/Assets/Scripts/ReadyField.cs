using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReadyField : MonoBehaviour
{
    private Button readyStartButton;

    private void Awake() {
        readyStartButton = GetComponentInChildren<Button>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            readyStartButton.onClick.Invoke();
        }
    }
}
