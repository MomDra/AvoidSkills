using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private int levelUpInterval;
    [SerializeField]
    private GameObject itemBallPrefab;

    private SkillLevel level = SkillLevel.LEVEL1;
    private float timer;
    
    private void Awake() {
        timer = 0;
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= levelUpInterval && (int) level < 3){
            timer = 0;
            ++level;
            ChangeColorByLevel();
        }
    }

    private void ChangeColorByLevel(){
        Color changeColor = GetComponent<Renderer>().material.color;
        switch (level){
            case SkillLevel.LEVEL1:
                break;
            case SkillLevel.LEVEL2:
                changeColor = Color.yellow;
                break;
            case SkillLevel.LEVEL3:
                changeColor = Color.red;
                break;
        }
        GetComponent<Renderer>().material.color = changeColor;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Projectile" || other.gameObject.tag == "Player"){
            GameObject clone = Instantiate(itemBallPrefab, transform.position, Quaternion.identity);
            clone.GetComponent<ItemBall>().skillLevel = level;

            Destroy(this.gameObject);
        }
    }

}
