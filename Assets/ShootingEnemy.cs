using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float moveSpeed = 1f;          // Enemy's movement speed
    public float shootCooldown = 1f; // Time between shots
    public float bulletSpeed = 7f;   // Speed of the bullet
    public float shootRange = 4f;   // Range to start shooting at the player
    public float bulletLifetime = 3f; // How long the bullet lives

    private Transform playerPos;     // player's position
    private float cooldownTimer;     // Timer to handle the cooldown between shots
    private Rigidbody2D rigBod;      // enemy's Rigidbody2D

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        rigBod = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component for movement
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position); // Calculate distance to the player

        if (distanceToPlayer <= shootRange) // If the player is within range
        {
            rigBod.linearVelocity = Vector2.zero;

            if (cooldownTimer <= 0) // If the enemy can shoot
            {
                ShootAtPlayer();
                cooldownTimer = shootCooldown; // Reset the cooldown timer
            }
            else
            {
                // Move the enemy towards the player
                cooldownTimer -= Time.deltaTime; // Countdown the cooldown timer
            }
        }
        else{
             MoveTowardsPlayer();
        }
    }

    // Move the enemy towards the player
    void MoveTowardsPlayer()
    {
        Vector2 direction = (playerPos.position - transform.position).normalized; // Calculate direction towards the player
        rigBod.linearVelocity = direction * moveSpeed; // Move the enemy in that direction
    }

    // Shoot a bullet towards the player
    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Instantiate the bullet at the enemy's position
        Vector2 direction = (playerPos.position - transform.position).normalized; // Get the direction towards the player

        TestBulletMovement bulletMovement = bullet.AddComponent<TestBulletMovement>(); // Add the BulletMovement component
        bulletMovement.Setup(direction, bulletSpeed, bulletLifetime); // Set up the bullet's direction, speed, and lifetime
    }
}

public class TestBulletMovement : MonoBehaviour
{
    private Vector2 direction;    // Direction the bullet will travel
    private float speed;          // Bullet speed
    private float lifeTime;       // Bullet lifetime
    private Rigidbody2D rigBod;   // Reference to the bullet's Rigidbody2D

    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        Destroy(gameObject, lifeTime); // Destroy the bullet after its lifetime
    }

    public void Setup(Vector2 _direction, float _speed, float _lifeTime)
    {
        direction = _direction; // Set the bullet's direction
        speed = _speed;         // Set the bullet's speed
        lifeTime = _lifeTime;   // Set the bullet's lifetime
    }

    void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rigBod.linearVelocity = direction * speed; // Move the bullet in the set direction
        }
    }
}
