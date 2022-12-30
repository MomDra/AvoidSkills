using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private SkillLevel level = SkillLevel.LEVEL1;
    private float timer;
    
    private void Awake() {
        timer = 0;
        Destroy(this.gameObject, EnvironmentManager.Instance.itemBoxDestroyTime);
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= EnvironmentManager.Instance.itemBoxLevelUpInterval && (int) level < 3){
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
            GameObject clone = Instantiate(EnvironmentManager.Instance.itemBallPrefab, transform.position, Quaternion.identity);
            clone.GetComponent<ItemBall>().skillLevel = level;

            Destroy(this.gameObject);
        }
    }

}
