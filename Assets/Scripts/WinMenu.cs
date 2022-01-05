using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
