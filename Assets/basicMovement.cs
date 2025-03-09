using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody2D rigBod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = rigBod.position;
        Vector2 difVec = mousePos - playerPos;

        Vector2 moveVector = new Vector2(hori * MoveSpeed, vert * MoveSpeed);
        float angle = math.degrees(math.atan2(difVec.x, difVec.y));

        rigBod.linearVelocity = moveVector;
        rigBod.SetRotation(-angle);
    }
}
