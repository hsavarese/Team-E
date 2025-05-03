using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private Text statsText;
    private PlayerStats playerStats;

    void Start()
    {
        // Find the player and get their stats component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
            Debug.Log("Player components: Health=" + (player.GetComponent<Health>() != null) + 
                     ", BasicShoot=" + (player.GetComponent<BasicShoot>() != null) + 
                     ", BasicMovement=" + (player.GetComponent<BasicMovement>() != null));
        }

        // Make sure the stats panel is hidden at start
        if (statsPanel != null)
        {
            statsPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle stats panel when Tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStatsPanel();
        }

        // Update stats text if panel is visible
        if (statsPanel != null && statsPanel.activeSelf && statsText != null && playerStats != null)
        {
            statsText.text = playerStats.toString();
        }
    }

    void ToggleStatsPanel()
    {
        if (statsPanel != null && playerStats != null)
        {
            bool isActive = !statsPanel.activeSelf;
            statsPanel.SetActive(isActive);
        }
    }
} 