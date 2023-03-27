using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project
{
    public class DirectionProvider : IMovementDirectionProvider
    {
        private Vector3 _direction;
        public DirectionProvider(Vector3 direction)
        {
            _direction = direction;
        }
        public Vector3 GetMovementDirection()
        {
            return _direction;
        }
    }
}