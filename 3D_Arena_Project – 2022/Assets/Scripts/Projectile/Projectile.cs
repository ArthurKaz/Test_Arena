using System;
using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project
{
    public abstract class Projectile : MonoBehaviour, IPullable
    {
        [Min(1)] [SerializeField] protected float speed = 10f;
        [Min(1)] [SerializeField]
        private float maxLifetime = 3f;

        [Min(1)] [SerializeField] protected float damage;
        private float _lifetime;
        
        private bool _isAvailable = true;
        
        public event Action<Projectile> Disappear;
        protected IMovementDirectionProvider Provider = new NullDirectionProvider();
        public int ID => 0;

        public void Init(IMovementDirectionProvider provider)
        {
            Provider = provider;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            MoveBullet();
            LifeTimeUpdate();
        }

        protected abstract void MoveBullet();

        private void LifeTimeUpdate()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime >= maxLifetime)
            {
                OnDisappear();
            } 
        }

        private void OnCollisionEnter(Collision other) => CollisionHandle(other);

        protected abstract void CollisionHandle(Collision other);
        
        public virtual void Activate()
        {
            _isAvailable = false;
            _lifetime = 0;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _isAvailable = true;
            gameObject.SetActive(false);
        }

        public bool IsAvailable()
        {
            return _isAvailable;
        }

        protected void OnDisappear()
        {
            Disappear?.Invoke(this);
        }
    }
}