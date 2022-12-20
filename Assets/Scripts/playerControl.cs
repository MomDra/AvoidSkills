using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : Unit
{
    private State state = State.STAND;

    [SerializeField]
    private GameObject arrow;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Attack();
        }
        Move();
    }

    void Attack(){
        isAttack = true;

        Vector3 arrowPos;
        Vector3 velocity = Vector3.zero;

        var clone = Instantiate(arrow, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = velocity;
    }

    void Move(){
        float moveDirX = Input.GetAxis("Horizontal");
        float moveDirZ = Input.GetAxis("Vertical");

        Vector3 vecX = transform.right * moveDirX;
        Vector3 vecY = transform.forward * moveDirZ;

        Vector3 velocity = (vecX + vecY).normalized * speed;

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
