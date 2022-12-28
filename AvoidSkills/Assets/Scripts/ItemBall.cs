using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBall : MonoBehaviour
{
    private SkillCode skillCode;
    public SkillLevel skillLevel{ private get; set; }

    private Rigidbody rigid;
    private System.Random random = new System.Random();

    private Vector3[] direction = new Vector3[8];

    private void Awake() {
        Destroy(this, 10f);
        rigid = GetComponent<Rigidbody>();

        direction[0] = new Vector3(0.05f, 0.1f, 0f);
        direction[1] = new Vector3(-0.05f, 0.1f, 0f);
        direction[2] = new Vector3(0f, 0.1f, 0.05f);
        direction[3] = new Vector3(0f, 0.1f, -0.05f);
        direction[4] = new Vector3(0.025f, 0.1f, 0.025f);
        direction[5] = new Vector3(-0.025f, 0.1f, -0.025f);
        direction[6] = new Vector3(-0.025f, 0.1f, 0.025f);
        direction[7] = new Vector3(0.025f, 0.1f, -0.025f);

        RandomDirection();
        RandomSkill();
    }

    private void RandomDirection(){
        rigid.AddForce(direction[random.Next(0, 7)] * Time.deltaTime, ForceMode.Impulse);
    }

    private void RandomSkill(){
        
        skillCode = (SkillCode) random.Next(2, 3);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<SkillManager>().slotList.isFull())
            {
                Debug.Log("아이템 슬롯이 가득 찼습니다!");
                return;
            }
            other.gameObject.GetComponent<SkillManager>().addItem(skillCode, skillLevel);
            Destroy(this.gameObject);
        }
    }
}
