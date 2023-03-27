using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project
{
    public abstract class ShootInput : MonoBehaviour
    {
        private ISpawnable<SimpleBullet> _bulletSpawnable;
        private Transform  _directionProvider;
        private IActionAfterHitChance _chance;
        private IKillRewardReceiver _receiver;

        public void Init(ISpawnable<SimpleBullet> bulletSpawnable, Transform directionProvider, IActionAfterHitChance chance, IKillRewardReceiver receiver)
        {
            _bulletSpawnable = bulletSpawnable;
            _directionProvider = directionProvider;
            _receiver = receiver;
            _chance = chance;
            
        }
        protected void SpawnBullet(Vector3 position)
        {
            var bullet = _bulletSpawnable.Spawn(position);
            bullet.Init(new DirectionProvider(_directionProvider.forward));
            bullet.Init(_receiver,_chance);
        }
    }
}