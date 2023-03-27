using UnityEngine;

namespace Test_Project
{
    public class MouseRotate : RotateInput
    {

        private void Update()
        {
            if (RuntimeController.LockedInput == false)
            {
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
                RotateEntity(mouseX);
                RotateCamera(mouseY);
            }
        }
    }
}