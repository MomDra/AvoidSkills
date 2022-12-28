using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class PlayerController : MonoBehaviour
    {
        private SkillManager skillManager = new SkillManager();

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClientSend.PlayerTargetPosition(MousePointer.Instance.MousePositionInWorld);
            }
        }

        private void FixedUpdate()
        {
        }
    }
}

