using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    
    private Transform playerPos;
    private Rigidbody2D rigBod;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = target.GetComponent<Transform>();
        rigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBod.MovePosition(Vector2.MoveTowards(rigBod.position, playerPos.position, moveSpeed));
    }
}
