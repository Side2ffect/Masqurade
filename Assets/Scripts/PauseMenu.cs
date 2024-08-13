using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        Paused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Paused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        Paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
