using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    
    private Rigidbody2D rigBod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBod.linearVelocity = transform.up * speed;
    }
}
