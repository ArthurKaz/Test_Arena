using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class KeyboardInput : MovementInput
    {
        [SerializeField] private KeyCode moveForward;
        private void Update()
        {
            if (Input.GetKey(moveForward))
            {
                MoveEntityForward();
            }
        }
    }
}