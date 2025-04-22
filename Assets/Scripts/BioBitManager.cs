using UnityEngine;
using UnityEngine.UI;

public class BioBitManager : MonoBehaviour
{
    public static BioBitManager Instance;

    public int bioBits = 0;
    public Text bioBitText;
    private const string SAVE_KEY = "BioBits";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBioBits();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBits(int amount)
    {
        bioBits += amount;
        UpdateUI();
        SaveBioBits();
    }

    void UpdateUI()
    {
        if (bioBitText != null)
            bioBitText.text = "BIO Bits: " + bioBits.ToString();
    }

    private void SaveBioBits()
    {
        PlayerPrefs.SetInt(SAVE_KEY, bioBits);
        PlayerPrefs.Save();
    }

    private void LoadBioBits()
    {
        bioBits = PlayerPrefs.GetInt(SAVE_KEY, 0);
        UpdateUI();
    }

    /*public void ResetBits() incase you need to reset them
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        bioBits = 0;
        UpdateUI();
    } */
}

