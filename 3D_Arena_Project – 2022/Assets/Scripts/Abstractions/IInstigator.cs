using UnityEngine;

namespace Test_Project.Abstractions
{
    public interface IInstigator
    {
        public T Instantiate<T>(T prefab) where T : MonoBehaviour;
    }
}