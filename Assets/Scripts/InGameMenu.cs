using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu; // the game object that represents the pause menu.
    // reload the current scene.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the first scene - aka the main menu.
    }
    // freeze the snake and show the pause menu.
    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        FindObjectOfType<Player>().SetIsMoving(true);
    }
}
