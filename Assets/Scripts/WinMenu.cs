using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    // return the user to the main menu.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
