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
    private Image lateHpBar;

    private bool isHpBarUpdate = false;

    private float reSyncFillAmount = 0;

    private void Awake()
    {
        usernameText = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        teamBar = images[0];
        hpBar = images[1];
        lateHpBar = images[2];
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
        if(!isHpBarUpdate) StartCoroutine(LateHpBarUpdate(_fillAmount));
        else reSyncFillAmount = _fillAmount;
    }

    private IEnumerator LateHpBarUpdate(float _fillAmount){
        isHpBarUpdate = true;
        float _fillAmountTargetValue = _fillAmount;
        float _decreaseAmount = (lateHpBar.fillAmount - _fillAmountTargetValue) / 10;
        for (int i = 0; i < 10;++i)
        {
            lateHpBar.fillAmount -= _decreaseAmount;
            yield return new WaitForSeconds(0.05f);
            if(reSyncFillAmount > 0){
                i = 0;
                lateHpBar.fillAmount = _fillAmountTargetValue;
                _fillAmountTargetValue = reSyncFillAmount;
                _decreaseAmount = (lateHpBar.fillAmount - _fillAmountTargetValue) / 10;
                reSyncFillAmount = 0;
            }
        }
        lateHpBar.fillAmount = _fillAmountTargetValue;
        isHpBarUpdate = false;
    }


}
