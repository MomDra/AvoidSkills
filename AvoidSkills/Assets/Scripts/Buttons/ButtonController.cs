using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController: MonoBehaviour
{

    public void Ready(){
        Debug.Log("Ready");
    }

    public void GameStart(){
        Debug.Log("Game Start");
    }

    public void Option(){
        Debug.Log("Option");
    }

    public void ShiftBlueTeam(){
        Debug.Log("Shift Blue Team");
    }

    public void ShiftRedTeam(){
        Debug.Log("Shift Red Team");
    }

    public void SettingRule(){
        Debug.Log("Setting Rule");
    }

    public void SelectCharacter(){
        Debug.Log("Select Character");
    }

    public void SelectNormalAttack(){
        Debug.Log("Select NormalAttack");
    }

    public void SelectCustomSkill(){
        Debug.Log("Select CustomSkill");
    }

    public void PrevMapImage(){
        Debug.Log("Prev Map Image");
    }

    public void NextMapImage(){
        Debug.Log("Next Map Image");
    }

    public void Exit(){
        Debug.Log("Exit");
        Application.Quit();
    }
}
