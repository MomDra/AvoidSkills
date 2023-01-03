using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController: MonoBehaviour
{

    public void Join(){
        string ip = GameObject.Find("Input_IP_Address").GetComponent<TMP_InputField>().text;
        string name = GameObject.Find("Input_Nickname").GetComponent<TMP_InputField>().text;
        Debug.Log("["+ip+"] " + name + " is trying to join the game");
    }

    public void Option(){
        Debug.Log("Option");
    } 

    public void Prev(){
        GameObject.Find("UserCustomSkills").GetComponent<UserCustomSkillDB>().Prev();
    }

    public void Next(){
        GameObject.Find("UserCustomSkills").GetComponent<UserCustomSkillDB>().Next();
    }

    public void Exit(){
        Debug.Log("Exit");
        Application.Quit();
    }
}
