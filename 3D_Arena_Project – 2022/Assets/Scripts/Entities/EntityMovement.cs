using System;
using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private IMovementDirectionProvider _direction;

        private void FixedUpdate()
        {
            if (RuntimeController.LockedInput == false)
            {
                Move();

            }
        }
        public void SetDirection(IMovementDirectionProvider direction)
        {
            _direction = direction;
        }
        private void Move()
        {
            Transform transform1 = transform;
            Vector3 worldDirection = transform1.TransformDirection(_direction.GetMovementDirection());
            transform1.localPosition += worldDirection * (speed * Time.deltaTime);
        }
    }
}