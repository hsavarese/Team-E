using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("DeathScreen Awake called");
        gameObject.SetActive(false); // hide the WHOLE DeathScreen canvas
    }

    public void ShowDeathScreen()
    {
        Debug.Log("ShowDeathScreen called");
        gameObject.SetActive(true); // show the WHOLE DeathScreen canvas
        Time.timeScale = 0f; // pause game
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
