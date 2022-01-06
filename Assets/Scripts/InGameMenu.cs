using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the first scene - aka the main menu.
    }
}
