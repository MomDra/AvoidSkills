using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance { get => instance; }

    public GameObject playerPrefab;
    public GameObject skillObjectPrefab;

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
        QualitySettings.vSyncCount = 0; // 수직동기화 끄기
        Application.targetFrameRate = 30; // 30 프레임 고정, 서버는 고 프레임이 필요 없기 때문임


        Server.Start(4, 26950);
    }

    private void LateUpdate() {
        
    }

    private void OnApplicationQuit()
    {
        Server.Stop();

        Debug.Log("OnApplicationQuit 호출됨!");
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
    }

    public SkillObject InstantiateSkillObject(Transform _shootOrigin)
    {
        return Instantiate(skillObjectPrefab, _shootOrigin.position + _shootOrigin.forward * 1.0f, Quaternion.identity).GetComponent<SkillObject>();
    }
}
