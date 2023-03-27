using UnityEngine;

namespace Test_Project
{
    public abstract class PauseInput : MonoBehaviour
    {
        public static bool GamePaused
        {
            get;
            private set;
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0;
            GamePaused = true;
            Cursor.lockState = CursorLockMode.None;
        }
        public void ResumeGame()
        {
            Time.timeScale = 1;
            GamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}