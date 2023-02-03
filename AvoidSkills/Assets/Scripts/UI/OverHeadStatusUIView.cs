using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverHeadStatusUIView : MonoBehaviour
{
    private Image hpBar;
    private TextMeshProUGUI usernameText;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Image>();
        usernameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetUsername(string _username){
        usernameText.SetText(_username);
    }

    public void SetHpBarFillAmount(float _fillAmount)
    {
        hpBar.fillAmount = _fillAmount;
    }
}
