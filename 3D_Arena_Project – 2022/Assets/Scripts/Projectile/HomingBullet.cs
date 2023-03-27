using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project
{
    public class HomingBullet : Projectile
    {
        private const float MinDistanceToTarget = 0.1f;
        public ITargetable Target;

        protected override void MoveBullet()
        {
            if (BulletHitTarget()) OnDisappear();
            var transform1 = transform;
            transform1.position += Provider.GetMovementDirection() * (speed * Time.deltaTime);

        }
        private bool BulletHitTarget()
        {
            return Vector3.Distance(transform.position, Target.GetPosition()) < MinDistanceToTarget;
        }

        protected override void CollisionHandle(Collision other)
        {
            if (other.transform.TryGetComponent(out IDieable entity))
            {
                entity.TakeDamage(damage);
            }
        }

        public void SetDirectionProvider()
        {
            Provider = new DirectionProvider(Provider.GetMovementDirection());
        }
    }
}