using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("DeathScreen Awake called");
        gameObject.SetActive(false); // hide the WHOLE DeathScreen canvas
    }

    public virtual void ShowDeathScreen()
    {
        if (IsRunningUnitTests())
        {
            Debug.Log("Running in Unit Test Mode — Skipping DeathScreen activation.");
            return;
        }

        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
    }

    public virtual void ReturnToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }

    public virtual void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    private bool IsRunningUnitTests()
    {
        // This still stays if you want, otherwise FakeDeathScreen handles skipping too
        return Application.isEditor && (Application.isBatchMode || System.Environment.CommandLine.Contains("-nographics"));
    }
}



