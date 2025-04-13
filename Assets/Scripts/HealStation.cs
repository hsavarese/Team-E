using UnityEngine;

public class HealStation : MonoBehaviour
{
    public int healAmount = 25;
    public float cooldownTime = 3f; // seconds
    public KeyCode interactKey = KeyCode.E;

    private bool isPlayerInRange = false;
    private bool isOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Optionally: Show "Press E to heal" UI
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Optionally: Hide UI
        }
    }

    private void Update()
    {
        if (isPlayerInRange && !isOnCooldown && Input.GetKeyDown(interactKey))
        {
            HealPlayer();
        }
    }

    private void HealPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            StartCoroutine(HealCooldown());
        }
    }

    private System.Collections.IEnumerator HealCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
