using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Transform camTransform;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClientSend.PlayerTargetPosition(MousePointer.Instance.MousePositionInWorld);
            }
        }

        private void FixedUpdate()
        {
            // SendInputToServer();
        }

        // private void SendInputToServer()
        // {
        //     bool[] _inputs = new bool[]
        //     {
        //     Input.GetKey(KeyCode.W),
        //     Input.GetKey(KeyCode.S),
        //     Input.GetKey(KeyCode.D),
        //     Input.GetKey(KeyCode.A),
        //     Input.GetKey(KeyCode.Space)
        // };

        //     ClientSend.PlayerMovement(_inputs);
        // }
    }
}

