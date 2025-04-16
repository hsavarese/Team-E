using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    
    private Transform playerPos;
    private Rigidbody2D rigBod;

    public GameObject BioBit;
    public int minBits = 1;
    public int maxBits = 10;
    
    private Health healthComponent;


    void Start()
    {
        healthComponent = GetComponent<Health>();
        target = GameObject.FindWithTag("Player"); 
        playerPos = target.transform; 
        rigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBod.MovePosition(Vector2.MoveTowards(rigBod.position, playerPos.position, moveSpeed));
        
    }

   public void Die()
{
    int bitsToDrop = Random.Range(minBits, maxBits + 1);

    for (int i = 0; i < bitsToDrop; i++)
{
    Vector3 dropPos = transform.position + new Vector3(
        Random.Range(-0.3f, 0.3f),
        Random.Range(-0.3f, 0.3f),
        0f
    );

    GameObject bit = Instantiate(BioBit, dropPos, Quaternion.identity);

//  gentle force to scatter it
    Rigidbody2D rb = bit.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        Vector2 force = Random.insideUnitCircle.normalized * Random.Range(0.5f, 1.2f);
        rb.AddForce(force, ForceMode2D.Impulse);

         // slow it down
        rb.linearDamping = 4f; // smooth glide then stop
        rb.angularDamping = 4f;
    }

    // Make sure it's only worth 1 point
    BioBitPickup pickup = bit.GetComponent<BioBitPickup>();
    if (pickup != null)
    {
        pickup.value = 1;
    }
}


    Destroy(gameObject);
}




}
