using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private int level = 1;

    private float timer;

    private void Awake() {
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 10 && level < 3){
            timer = 0;
            ++level;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Destroy(this);
        }
    }
}
