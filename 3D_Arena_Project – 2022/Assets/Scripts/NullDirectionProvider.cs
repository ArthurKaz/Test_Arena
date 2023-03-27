using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public class NullDirectionProvider : IMovementDirectionProvider
    {
        public Vector3 GetMovementDirection()
        {
            return Vector3.forward;
        }
    }
}