using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.Entities
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected ITargetable Targetable;
        private ISpawnable<HomingBullet> _bulletSpawn;
        protected IDieable Dieable;
        public virtual void Init(ITargetable targetable)
        {
            Targetable = targetable;
            TargetDirection.Targetable = targetable;
        }
        public void Init(IDieable dieable)
        {
            Dieable = dieable;
        }

        public abstract void StartAttack();
        public abstract void StopAttack();
        
        public virtual void Init(ISpawnable<HomingBullet> bulletSpawnable)
        {
        }
    }
}