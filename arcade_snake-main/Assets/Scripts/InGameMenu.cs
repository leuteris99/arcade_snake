using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the first scene - aka the main menu.
    }

    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        FindObjectOfType<Player>().SetIsMoving(true);
    }
}
