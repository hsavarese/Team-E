using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    
    private Transform playerPos;
    private Rigidbody2D rigBod;
    

    void Start()
    {
        target = GameObject.FindWithTag("Player"); 
        playerPos = target.transform; 
        rigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBod.MovePosition(Vector2.MoveTowards(rigBod.position, playerPos.position, moveSpeed));
        
    }
}
