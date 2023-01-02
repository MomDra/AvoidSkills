using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectUIView : MonoBehaviour
{
    public static ConnectUIView instance;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField ipAddresssField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects!"); ;
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.Instance.ConnectToServer(ipAddresssField.text, usernameField.text);
    }
}
