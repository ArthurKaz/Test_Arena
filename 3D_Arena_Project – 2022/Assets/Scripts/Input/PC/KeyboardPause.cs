using UnityEngine;
using UnityEngine.UI;

namespace Test_Project
{
    public class KeyboardPause : PauseInput
    {
        [SerializeField] private KeyCode pause = KeyCode.Escape;
        [SerializeField] private GameObject pausedMenu;
        [SerializeField] private Button resumeButton;

        private void Start()
        {
            ResumeGame();
            resumeButton.onClick.AddListener(HandlePause);
            pausedMenu.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKey(pause))
            {
                HandlePause();
            }
        }

        private void HandlePause()
        {
            if (GamePaused)
            {
                ResumeGame();
                HidePausedMenu();
            }
            else
            {
                PauseGame();
                ShowPausedMenu();
            }
        }

    private void HidePausedMenu()
        {
            pausedMenu.SetActive(false);
        }
        private void ShowPausedMenu()
        {
            pausedMenu.SetActive(true);
        }
    }
}