using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverHeadStatusUIView : MonoBehaviour
{
    private TextMeshProUGUI usernameText;
    private Image teamBar;
    private Image hpBar;

    private void Awake()
    {
        usernameText = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        teamBar = images[0];
        hpBar = images[1];
    }

    public void SetUsername(string _username, bool _isRed)
    {
        usernameText.SetText(_username);

        if (_isRed) teamBar.color = Color.red;
        else teamBar.color = Color.blue;
    }

    public void SetHpBarFillAmount(float _fillAmount)
    {
        hpBar.fillAmount = _fillAmount;
    }
}
