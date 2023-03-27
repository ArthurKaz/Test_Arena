using System.Collections;
using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.Entities
{
    public class BlueEnemyAttackBehaviour : EnemyBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootDelay = 1;
        [SerializeField] private Persecutor persecutor;
        
        private ISpawnable<HomingBullet> _bulletSpawn;
        private IEnumerator _shooting;
        public bool isActive;


        public override void Init(ITargetable targetable)
        {
            base.Init(targetable);
            persecutor.Init(targetable);
        }

        public override void Init(ISpawnable<HomingBullet> bulletSpawnable)
        {
            _bulletSpawn = bulletSpawnable;
        }
        public override void StartAttack()
        {
            if (_shooting != null)
            {
                StopCoroutine(_shooting);
            }
            _shooting = Shooting();
            isActive = true;
            StartCoroutine(_shooting);
        }
        public override void StopAttack()
        {
            if (_shooting != null)
            {
                StopCoroutine(_shooting);
            }
        }
        private IEnumerator Shooting()
        {
            while (isActive)
            {
                yield return new WaitForSeconds(shootDelay);
                if (persecutor.TargetInRange())
                {
                    SpawnBullet();
                }
            }
        }
        private void SpawnBullet()
        {
            var target = Targetable.GetPosition();
            target.y = transform.position.y;
            transform.LookAt(target);
            var bullet = _bulletSpawn.Spawn(shootPoint.position);
            bullet.Target = Targetable;
            var direction = new TargetDirection(bullet.transform);
            bullet.Init(direction);
        }
    }
}