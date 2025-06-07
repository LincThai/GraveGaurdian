using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // unlock cursor when in the main menu scene
        Cursor.lockState = CursorLockMode.Confined;
    }

    // starts the game
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // quits the game leaving the closing the application
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
