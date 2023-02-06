using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    private float applySpeed;

    public PlayerStatus status;

    Vector3 targetPos;

    private bool isSpanwd;
    private bool isRed;
    public bool IsRed { get => isRed; }

    private void Awake()
    {
        status = GetComponent<PlayerStatus>();

        applySpeed = status.moveSpeed * Time.fixedDeltaTime;
        targetPos = Vector3.zero;
    }

    public void Initialize(int _id, string _username, Vector3 _pos, bool _isRed)
    {
        id = _id;
        username = _username;
        isSpanwd = true;

        transform.position = _pos;
        targetPos = _pos;
        isRed = _isRed;
    }

    public void FixedUpdate()
    {
        if (!IsDestination())
        {
            Move();
        }

        CheckInMap();
    }

    private void Move()
    {
        Vector3 _moveDirection = (targetPos - transform.position).normalized;
        _moveDirection *= applySpeed;

        controller.Move(_moveDirection);

        ServerSend.PlayerPosition(this);
    }

    public void SetTargetPos(Vector3 _targetPos)
    {
        targetPos = _targetPos;
    }

    private bool IsDestination()
    {
        return Vector3.Distance(transform.position, targetPos) < 0.15f;
    }

    public void ShootSkill(SkillCode _skillCode, SkillLevel _skillLevel, Vector3 _mousePos, bool _isItemSkill)
    {
        if(_isItemSkill) ServerSend.InstantiateSkillCastEffect(transform.position);
        SkillDB.Instance.GetSkill(_skillCode, _skillLevel).cmd(this, status, _mousePos);
    }

    public void TakeDamage(int _damage)
    {
        if (status.hp <= 0f)
        {
            return;
        }

        status.hp -= _damage;
        if (status.hp <= 0)
        {
            isSpanwd = false;
            status.hp = 0;
            controller.enabled = false;
            transform.position = new Vector3(0f, 25f, 0f);
            ServerSend.PlayerPosition(this);

            Server.gameRoom.inGameRoom.DeadPlayer(id);

            StartCoroutine(Respawn());  
        }

        ServerSend.PlayerHealth(this);
    }

    private void Dead()
    {
        isSpanwd = false;
        status.hp = 0;
        controller.enabled = false;
        transform.position = new Vector3(0f, 25f, 0f);
        ServerSend.PlayerPosition(this);

        Server.gameRoom.inGameRoom.DeadPlayer(id);

        StartCoroutine(Respawn());

        ServerSend.PlayerHealth(this);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        isSpanwd = true;
        status.hp = status.maxHp;
        controller.enabled = true;
        ServerSend.PlayerRespawned(this);
    }

    private void CheckInMap()
    {
        if (Mathf.Abs(transform.position.x) > 21.5f || Mathf.Abs(transform.position.z) > 13.5f && isSpanwd == true)
        {
            Dead();
        }
    }
}
