using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance { get => instance; }

    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("Build the project to start the server!");
#else
        Server.Start(10, 26950);
#endif
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    }
}
