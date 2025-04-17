using UnityEngine;
using UnityEngine.UI;

public class BioBitManager : MonoBehaviour
{
    public static BioBitManager Instance;

    public int bioBits = 0;
    public Text bioBitText;

    void Awake()
    {
        Instance = this;
    }

    public void AddBits(int amount)
    {
        bioBits += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (bioBitText != null)
            bioBitText.text = "BIO Bits: " + bioBits.ToString();
    }
}

