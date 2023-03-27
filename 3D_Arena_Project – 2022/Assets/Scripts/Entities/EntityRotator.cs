using UnityEngine;

namespace Test_Project.Entities
{
    public class EntityRotator : MonoBehaviour
    {
        public void Rotate(Quaternion eulers)
        {
            transform.localRotation = eulers;
        }
        public void Rotate(Vector3 eulers)
        {
            transform.Rotate(eulers);
        }
    }
}