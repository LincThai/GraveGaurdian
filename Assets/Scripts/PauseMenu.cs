using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // set variables   
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        // when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // check if game is paused
            if (GameIsPaused)
            {
                // call resume function
                Resume();
            }
            else
            {
                // call pause function
                Pause();
            }
        }
    }

    // Resume pausing and hiding UI
    void Resume()
    {
        // disable pause menu ui
        pauseMenuUI.SetActive(false);
        // set time scale to 1 for standard time movement
        Time.timeScale = 1f;
        // change bool to false
        GameIsPaused = false;
        // lock the cursor to the center
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Method of pausing the game
    void Pause()
    {
        // activate ther pause menu.
        pauseMenuUI.SetActive(true);
        //Stop time by changing time scale to 0
        Time.timeScale = 0f;
        // set the bool to true
        GameIsPaused = true;
        // unlock the cursor
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu()
    {
        // so when you return to this menu from pause the game time is restarted
        Time.timeScale = 1f;
        // Loads the title screen which should always be scene index 0
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("quitting game..");
        Application.Quit();
    }
}
