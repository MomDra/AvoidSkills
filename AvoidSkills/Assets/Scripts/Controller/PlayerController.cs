using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class PlayerController : MonoBehaviour
    {
        private SkillManager skillManager;
        private PlayerStatus status;

        private void Awake()
        {
            skillManager = GetComponent<SkillManager>();
            status = GetComponent<PlayerStatus>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClientSend.PlayerTargetPosition(MousePointer.Instance.MousePositionInWorld);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                skillManager.NormalAttack();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                skillManager.UserCustomSkill();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillManager.ItemSkill1();
            }

            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     skillManager.ItemSkill2();
            // }
            // if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            //     skillManager.ItemSkill3();
            // }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ClientSend.PlayerTargetPosition(transform.position);
            }
        }

        private void FixedUpdate()
        {
        }
    }
}

