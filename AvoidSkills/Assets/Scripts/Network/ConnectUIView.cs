using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectUIView : MonoBehaviour
{
    public static ConnectUIView instance;
    private TMP_InputField usernameField;
    private TMP_InputField ipAddresssField;
    private Button joinButton;
    private Button quitButton;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            TMP_InputField[] tMP_InputFields = GetComponentsInChildren<TMP_InputField>();

            usernameField = tMP_InputFields[0];
            ipAddresssField = tMP_InputFields[1];
            Button[] buttons = GetComponentsInChildren<Button>();
            joinButton = buttons[0];
            quitButton = buttons[1];
            buttonText = joinButton.GetComponentInChildren<TextMeshProUGUI>();

            joinButton.onClick.AddListener(ConnectToServer);
            quitButton.onClick.AddListener(QuitGame);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects!"); ;
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        StartCoroutine(ConnectToServerCoroutine());

        joinButton.interactable = false;
        ipAddresssField.interactable = false;
        usernameField.interactable = false;

        Client.Instance.ConnectToServer(ipAddresssField.text, usernameField.text);
    }

    private IEnumerator ConnectToServerCoroutine()
    {
        for (int i = 5; i >= 0; --i)
        {
            buttonText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        buttonText.text = "Join";

        joinButton.interactable = true;
        ipAddresssField.interactable = true;
        usernameField.interactable = true;
    }

    private void QuitGame()
    {
        Application.Quit(1);
    }
}
