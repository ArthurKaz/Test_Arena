using Test_Project.Abstractions;
using UnityEngine;
namespace Test_Project
{
    public abstract class MovementInput : MonoBehaviour, IMovementDirectionProvider
    {
        private EntityMovement _entityMovement;
        public void Init(EntityMovement entityMovement)
        {
            _entityMovement = entityMovement;
            _entityMovement.SetDirection(this);
        }
        public abstract Vector3 GetMovementDirection();
    }
}