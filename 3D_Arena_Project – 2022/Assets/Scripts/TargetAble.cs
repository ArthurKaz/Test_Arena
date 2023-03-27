using System;
using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.TestEnemy
{
    public class TargetAble : MonoBehaviour, ITargetable
    {
        public event Action Replaced;
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual void OnReplaced()
        {
            Replaced?.Invoke();
        }
    }
}