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
        id = nextItemBallId;
        ++nextItemBallId;
        itemBalls.Add(id, this);

        Debug.Log("key: " + id);
        StartCoroutine(DestroySelf());
    }

    public void Initialize(int _level)
    {
        skillLevel = (SkillLevel)_level;
        SetRandomSkillCode();
        ServerSend.InstantiateItemBall(this);  
    }

    private void SetRandomSkillCode(){
        skillCode = (SkillCode)new System.Random().Next((int)SkillDB.Instance.skillCodeStartIndex, (int)SkillDB.Instance.skillCodeEndIndex + 1);
    }

    private void OnTriggerEnter(Collider other) {
        Player otherPlayer = other.gameObject.GetComponent<Player>(); 
        if(otherPlayer!=null){
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

    public static void Clear(){
        foreach(KeyValuePair<int,ItemBall> item in itemBalls){
            Destroy(item.Value);
        }
        itemBalls.Clear();
        nextItemBallId = 1;
    }
}
