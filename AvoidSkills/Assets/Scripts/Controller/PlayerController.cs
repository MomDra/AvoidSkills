using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class PlayerController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            SendInputToServer();
        }

        private void SendInputToServer()
        {
            bool[] _inputs = new bool[]
            {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.A)
            };

            ClientSend.PlayerMovement(_inputs);
        }
    }
}

