using UnityEngine;
using Random = UnityEngine.Random;
using Test_Project.Abstractions;

namespace Test_Project
{
    public class SimpleBullet : Projectile
    {
        [SerializeField] private bool enemyRebound;
        [SerializeField] private bool obstacleRebound;

        private bool _rebounded;

        private IKillRewardReceiver _receiver;
        private IActionAfterHitChance _actionAfterHitChance = new NullActionAfterHitChance();
        
        public void Init(IKillRewardReceiver receiver, IActionAfterHitChance actionAfterHitChance)
        {
            _receiver = receiver;
            _actionAfterHitChance = actionAfterHitChance;
        }
        protected override void MoveBullet()
        {
            var transform1 = transform;
            transform1.position += Provider.GetMovementDirection() * (speed * Time.deltaTime);
        }
        protected override void CollisionHandle(Collision other)
        {
            var reboundDirection = GetReboundDirection(other.collider);
            if (other.transform.TryGetComponent(out IDieable entity))
            {
                HitEntity(entity);
            }
            AfterHitAction(reboundDirection, entity);
        }
        
        private Vector3 GetReboundDirection(Collider collider)
        {
            Vector3 closestPoint = collider.ClosestPoint(transform.position);
            return Vector3.Reflect(Provider.GetMovementDirection(), (closestPoint - transform.position).normalized);
        }

        private void HitEntity(IDieable entity)
        {
            if (_rebounded)
            {
                _receiver.ReceiveRewardForReboundKill();
            }
            entity.TakeDamage(entity.MaxHealth);
            _receiver.ReceiveKillReward(entity.Worth);
        }

        private void AfterHitAction(Vector3 reboundDirection, IDieable enemy)
        {
            if (_actionAfterHitChance.DidAfterHit())
            {
                if (Random.value < 0.5f)
                {
                    ReboundHandle(reboundDirection, enemy != null);
                }
                else
                {
                    OnDisappear();
                }
            }
            else
            {
                OnDisappear();
            }
        }

        private void ReboundHandle(Vector3 reboundDirection, bool hitEnemy)
        {
            if (enemyRebound && hitEnemy)
            {
                Provider = new StaticDirection(reboundDirection);
                _rebounded = true;
            }
            else if (obstacleRebound && hitEnemy == false)
            {
                Provider = new StaticDirection(reboundDirection);
            }
            else
            {
                OnDisappear();
            }
        }
        public override void Activate()
        {
            base.Activate();
            _rebounded = false;
        }
    }
}