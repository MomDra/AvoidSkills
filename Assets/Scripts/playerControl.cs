using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : Unit
{
    private State state = State.STAND;

    [SerializeField]
    private GameObject arrowPrefab;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Attack()
    {
        Vector3 arrowPos = transform.position + (MousePointer.Instance.MousePositionInWorld - transform.position).normalized;
        Vector3 velocity = (MousePointer.Instance.MousePositionInWorld - arrowPos).normalized * arrowSpeed;

        GameObject clone = Instantiate(arrowPrefab, arrowPos, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = velocity;
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(moveDirX, 0f, moveDirZ);
        Vector3 velocity = moveVec.normalized * moveSpeed;

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
