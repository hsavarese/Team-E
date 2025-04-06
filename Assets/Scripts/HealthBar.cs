using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject player;
    private Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        print(playerHealth.getHealthPercent());
        transform.localScale = new Vector3(playerHealth.getHealthPercent(), 1, 1);
    }
}
