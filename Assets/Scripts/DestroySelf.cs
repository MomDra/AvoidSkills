using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField]
    private string[] ignoreTags;

    void Awake()
    {
        Destroy(this.gameObject, 0.8f);
    }



    private void OnCollisionEnter(Collision other)
    {
        for (int i = 0; i < ignoreTags.Length; ++i){
            if(other.gameObject.tag == ignoreTags[i]){
                return;
            }
        }
        Destroy(this.gameObject);
    }
}
