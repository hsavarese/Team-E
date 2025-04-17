using Unity.Mathematics;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    const float MIN_SPEED = 2;
    const float MAX_SPEED = 10;
    private float moveSpeed;

   /*
    public Sprite still;
    public Sprite forward;
    public Sprite diagonal;
    */
    private Rigidbody2D rigBod;
    private SpriteRenderer spriteRend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
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

        rigBod.angularVelocity = 0;
        rigBod.linearVelocity = moveVector;
        rigBod.SetRotation(-angle);
    }

    public float setMoveSpeed(float newSpeed){
        moveSpeed = newSpeed;
        if(moveSpeed > MAX_SPEED) 
            moveSpeed = MAX_SPEED;
        else if(moveSpeed < MIN_SPEED) 
            moveSpeed = MIN_SPEED;
        return moveSpeed;
    }
}
