using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class Pause_menu : MonoBehaviour
{

    public GameObject PausePanel;
    public GameObject Dark_Overlay;

    public static bool isPaused;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PausePanel == null)
        {
            Debug.LogError("PausePanel is not assigned in the inspector!");
        }
        if (Dark_Overlay == null)
        {
            Debug.LogError("Dark_Overlay is not assigned in the inspector!");
        }
        
        if (PausePanel != null) PausePanel.SetActive(false);
        if (Dark_Overlay != null) Dark_Overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update running"); //These are tests to make sure the esp key is being read
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
        if (PausePanel == null || Dark_Overlay == null)
        {
            Debug.LogError("Cannot pause game: UI elements are not properly assigned!");
            return;
        }

        PausePanel.SetActive(true);
        Dark_Overlay.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ResumeGame()
    {
        if (PausePanel == null || Dark_Overlay == null)
        {
            Debug.LogError("Cannot resume game: UI elements are not properly assigned!");
            return;
        }

        PausePanel.SetActive(false);
        Dark_Overlay.SetActive(false);
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

    public bool getIsPaused(){
        return isPaused;
    }
}

