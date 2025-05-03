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
        
        PausePanel.SetActive(true);
        Dark_Overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update running"); //These are tests to make sure the esp key is being read
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("Escape key pressed");
        if (!isPaused)
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
        PausePanel.SetActive(true);
        Dark_Overlay.SetActive(true);
        Time.timeScale = 1f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Dark_Overlay.SetActive(false);
        Time.timeScale = 0f;
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

