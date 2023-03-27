using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.Entities
{
    public class Enemy : Entity, IPullable
    {
        [SerializeField] private EnemyBehaviour enemyAttackBehaviour;
        private bool _isAvailable;

        public void Init(ITargetable targetable)
        {
            enemyAttackBehaviour.Init(targetable);
        }
        public void Init(ISpawnable<HomingBullet> bulletSpawn)
        {
            enemyAttackBehaviour.Init(bulletSpawn);
            enemyAttackBehaviour.Init(this);
        }
        public void Activate()
        {
            _isAvailable = false;
            gameObject.SetActive(true);
            enemyAttackBehaviour.StartAttack();
            Reset();
        }
        public void Deactivate()
        {
            _isAvailable = true;
            gameObject.SetActive(false);
            enemyAttackBehaviour.StopAttack();
        }
        public bool IsAvailable()
        {
            return _isAvailable;
        }
    }
}