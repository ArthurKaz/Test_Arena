using System;
using UnityEngine;

namespace Test_Project.Abstractions
{
    public interface IDieable 
    {
        public event Action Died;
        public event Action TookDamage;
       // public float Health { get; }
        public bool Alive { get; }
        public float Worth { get; }
        public float MaxHealth{ get; }
        Transform Transform { get;}
        public void TakeDamage(float damage);
    }
}