using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{

    public GameObject PauseMenu;

    public static bool isPaused;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update running");
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("Escape key pressed");
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

        
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void loadMainMenu()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Main Menu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit(); // will only work if you build and run the application
    }
}

