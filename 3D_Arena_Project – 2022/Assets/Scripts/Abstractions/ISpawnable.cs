using UnityEngine;

namespace Test_Project.Abstractions
{
    public interface ISpawnable<T>
    {
        public T Spawn(Vector3 position);
    }
}