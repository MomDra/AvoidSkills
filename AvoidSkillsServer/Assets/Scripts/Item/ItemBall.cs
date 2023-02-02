using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBall : MonoBehaviour
{
    public static Dictionary<int, ItemBall> itemBalls = new();
    private static int nextItemBallId = 1;

    public int id;
    [HideInInspector]
    public SkillCode skillCode;
    [HideInInspector]
    public SkillLevel skillLevel;

    [SerializeField]
    private int destroyTime;

    void Awake()
    {
        Debug.Log($"before nextItemBallId : {nextItemBallId}");
        id = nextItemBallId;
        Debug.Log($"set id : {id}");
        Debug.Log($"after nextItemBallId : {nextItemBallId}");
        ++nextItemBallId;
        itemBalls.Add(id, this);

        Debug.Log("key: " + id);
        StartCoroutine(DestroySelf());
    }

    public void Initialize(int _level)
    {
        skillLevel = (SkillLevel)_level;
        SetRandomSkillCode();
        Debug.Log($"before id : {id}");
        ServerSend.InstantiateItemBall(this);  
    }

    private void SetRandomSkillCode(){
        skillCode = (SkillCode)new System.Random().Next((int)SkillDB.Instance.skillCodeEndIndex, (int)SkillDB.Instance.skillCodeStartIndex + 1);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger / itemBall Collision");
        Player otherPlayer = other.gameObject.GetComponent<Player>(); 
        if(otherPlayer!=null){
            Debug.Log($"Trigger / player and itemBall collision : {otherPlayer.id}, {id}");
            ServerSend.GainItemBall(otherPlayer.id, id);
            Destroy();
        }
    }

    private void Destroy(){
        itemBalls.Remove(id);
        ServerSend.DestroyItemBall(id);
        Destroy(gameObject);
    }

    private IEnumerator DestroySelf(){
        yield return new WaitForSeconds(destroyTime);
        Destroy();
    }
}
