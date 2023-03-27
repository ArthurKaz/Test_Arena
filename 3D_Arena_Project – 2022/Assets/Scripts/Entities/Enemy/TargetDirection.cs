using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project.Entities
{
    public class TargetDirection : IMovementDirectionProvider
    {
        public static ITargetable Targetable;
        private readonly Transform _transform;

        public TargetDirection(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 GetMovementDirection()
        {
            Vector3 direction = Targetable.GetPosition() - _transform.position;
            direction.Normalize();
            return direction;
        }
    }
}