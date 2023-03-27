using UnityEngine;

namespace DefaultNamespace
{
    public class MouseLook : MonoBehaviour
    {
        public float sensitivity = 100f;
        public Transform playerBody;
        private float xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}