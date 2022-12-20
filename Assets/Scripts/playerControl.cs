using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : Unit
{
    private State state = State.STAND;

    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private MousePointer mouse;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Attack();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    void Attack(){
        Vector3 arrowPos = transform.position + (mouse.worldPosition - transform.position).normalized;
        Vector3 velocity = (mouse.worldPosition - arrowPos).normalized * arrowSpeed;

        GameObject clone = Instantiate(arrow,  arrowPos, Quaternion.identity);
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
