using UnityEngine;

namespace Test_Project.Abstractions
{
    public interface IMovementDirectionProvider
    {
        public Vector3 GetMovementDirection();
    }
}