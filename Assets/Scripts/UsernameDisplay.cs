using UnityEngine;
using UnityEngine.UI;

public class UsernameDisplay : MonoBehaviour
{
    public Text usernameText;

    void Start()
    {
        string username = PlayerPrefs.GetString("PLAYER_NAME", "Guest");
        if (usernameText != null)
        {
            usernameText.text = "Username: " + username;
        }
    }
}

