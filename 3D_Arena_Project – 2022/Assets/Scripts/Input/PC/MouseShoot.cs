using UnityEngine;

namespace Test_Project
{
    public class MouseShoot : ShootInput
    {
         [SerializeField] private new Camera camera;
         [SerializeField] private float distanceFromScreenToBulletSpawnPoint;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && RuntimeController.LockedInput == false)
            {
                SpawnBullet(GetCenterOfScreen());
            }
        }

        private Vector3 GetCenterOfScreen()
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            return camera.ScreenToWorldPoint(new Vector3(x, y,distanceFromScreenToBulletSpawnPoint));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetCenterOfScreen(),0.01f);
        }
    }
}