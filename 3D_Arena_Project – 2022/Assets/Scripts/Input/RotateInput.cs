using UnityEngine;
using Test_Project.Entities;

namespace Test_Project
{
    public abstract class RotateInput : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 100;
        [SerializeField] private EntityRotator entityRotator;
        [SerializeField] private new EntityRotator camera;
        [SerializeField] private float maxVerticalAngle = 90f;


        private float _currentVerticalAngle;

        protected void RotateEntity(float xInput)
        {
            float horizontalRotation = xInput * sensitivity;
            entityRotator.Rotate(new Vector3(0, horizontalRotation, 0));
        }

        protected void RotateCamera(float yInput)
        {
            float verticalRotation = yInput * sensitivity;
            _currentVerticalAngle =
                Mathf.Clamp(_currentVerticalAngle - verticalRotation, -maxVerticalAngle, maxVerticalAngle);
            camera.Rotate(Quaternion.Euler(_currentVerticalAngle, 0f, 0f));
        }
    }
}