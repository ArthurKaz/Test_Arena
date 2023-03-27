using System;
using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.Entities
{
    public abstract class Entity : MonoBehaviour, IDieable
    {
        [SerializeField] protected ReversibleFloat health;
        [Min(0)] [SerializeField] private float worth;
        public float Health => health.Value;
        public bool Alive => Health > 0;
        public float Worth => worth;
        public float MaxHealth => health.Max;
        public Transform Transform => transform;

        public event Action Died;
        public event Action TookDamage;

        protected void Reset()
        {
            health.Init(health.Max);
        }

        protected void Heal()
        {
            health.Value -= MaxHealth / 2;
        }
        public void TakeDamage(float damage)
        {
            health.Value -= damage;
            UpdateState();
        }

        private void UpdateState()
        {
            OnTookDamage();
            if (Alive == false )
            {
                OnDied();
            }
        }
        private void OnTookDamage()
        {
            TookDamage?.Invoke();
        }

        private void OnDied()
        {
            Died?.Invoke();
        }
    }
}