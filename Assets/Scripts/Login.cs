using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField nameInput;         
    public GameObject loginMenuPanel;     

    private const string UsernameKey = "PLAYER_NAME";
    private const string UsernameLogKey = "USERNAME_LOG";

    void Start()
    {
        // Skip login if a name is already saved
        if (PlayerPrefs.HasKey(UsernameKey))
        {
            //loginMenuPanel.SetActive(false);
        }
    }

    public void OnCreateUsernamePressed()
    {
        string username = nameInput.text.Trim();

        if (!string.IsNullOrEmpty(username))
        {
            // Save username
            PlayerPrefs.SetString(UsernameKey, username);

            //adding username to log
            string log = PlayerPrefs.GetString(UsernameLogKey, "");
            if (!log.Contains(username + ";")) // avoids matches
            {
                log += username + ";";
                PlayerPrefs.SetString(UsernameLogKey, log);
            }

            PlayerPrefs.Save();
            loginMenuPanel.SetActive(false);
            Debug.Log("Username saved: " + username);

            SceneManager.LoadScene("Main Menu");
        }
    }

    public void OnSkipLoginPressed()
    {
        loginMenuPanel.SetActive(false);
        Debug.Log("Login skipped.");
    }

    //print usernames button
    public void PrintUsernameLog()
    {
        string log = PlayerPrefs.GetString(UsernameLogKey, "");
        string[] usernames = log.Split(';');

        Debug.Log("Saved Usernames:");
        foreach (string name in usernames)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Debug.Log("• " + name);
        }
    }
}


