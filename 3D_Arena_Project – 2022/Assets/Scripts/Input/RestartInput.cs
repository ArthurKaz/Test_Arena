using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test_Project
{
    public abstract class RestartInput : MonoBehaviour
    {
        protected void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}