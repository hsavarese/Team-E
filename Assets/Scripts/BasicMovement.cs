using Unity.Mathematics;
//using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    const float MIN_SPEED = 2;
    const float MAX_SPEED = 10;
    private float moveSpeed = 5f; // Initialize default move speed
    private float dashSpeed = 20f; // Dash speed multiplier
    private float dashDuration = 0.2f; // How long the dash lasts
    private float dashCooldown = 1f; // Cooldown between dashes
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    private bool isDashing = false;

    /*
     public Sprite still;
     public Sprite forward;
     public Sprite diagonal;
     */
    private Rigidbody2D rigBod;
    private SpriteRenderer spriteRend;
    private Collider2D playerCollider;
    private Color originalColor;
    private Color invincibleColor = new Color(1f, 1f, 1f, 0.5f); // Semi-transparent white
    private int enemyLayer;
    private bool isCollisionDisabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        originalColor = spriteRend.color;
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = rigBod.position;
        Vector2 difVec = mousePos - playerPos;

        /*
        if(hori == 0 && vert == 0){
            spriteRend.sprite = still;
        }else if(vert != 0 && hori != 0){
            spriteRend.sprite = diagonal;
        }else{
            spriteRend.sprite = forward;
        }
        */

        Vector2 moveVector = new Vector2(hori * moveSpeed, vert * moveSpeed);
        float angle = math.degrees(math.atan2(difVec.x, difVec.y));

        // Handle dash input and cooldown
        if (Input.GetKey(KeyCode.LeftShift) && cooldownTimer <= 0 && !isDashing && moveVector.magnitude > 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;
            spriteRend.color = invincibleColor; // Visual feedback for invincibility

            // Disable collision with enemies
            if (!isCollisionDisabled)
            {
                isCollisionDisabled = true;
                Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);
            }
        }

        // Apply dash if active
        if (isDashing)
        {
            moveVector = moveVector.normalized * dashSpeed;
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                spriteRend.color = originalColor; // Restore original color

                // Re-enable collision with enemies
                if (isCollisionDisabled)
                {
                    isCollisionDisabled = false;
                    Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
                }
            }
        }

        // Update cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.fixedDeltaTime;
        }

        rigBod.angularVelocity = 0;
        rigBod.linearVelocity = moveVector;
        rigBod.SetRotation(-angle);
    }

    public float setMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        if (moveSpeed > MAX_SPEED)
            moveSpeed = MAX_SPEED;
        else if (moveSpeed < MIN_SPEED)
            moveSpeed = MIN_SPEED;
        return moveSpeed;
    }

    // Public method to check if player is dashing (for damage calculations)
    public bool IsDashing()
    {
        return isDashing;
    }

    public float getSpeed()
    {
        return moveSpeed;
    }
}
