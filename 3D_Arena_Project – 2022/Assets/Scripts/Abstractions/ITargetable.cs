using System;
using UnityEngine;

namespace Test_Project.Abstractions
{
    public interface ITargetable
    {
        public event Action Replaced;
        public Vector3 GetPosition();
    }
}