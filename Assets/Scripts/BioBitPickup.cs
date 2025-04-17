using UnityEngine;

public class BioBitPickup : MonoBehaviour
{
    public int value = 1;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BioBitManager.Instance.AddBits(value);
            Destroy(gameObject);
        }
    }
}
