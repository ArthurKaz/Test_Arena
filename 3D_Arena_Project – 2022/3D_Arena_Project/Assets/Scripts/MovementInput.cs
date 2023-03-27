using UnityEngine;

namespace DefaultNamespace
{
    public abstract class MovementInput : MonoBehaviour
    {
        [SerializeField] private EntityMovement _entityMovement;

        protected void MoveEntityForward()
        {
            _entityMovement.MoveForward();
        }
    }
}