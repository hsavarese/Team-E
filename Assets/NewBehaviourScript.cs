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
        //float mouseX = Input.GetAxisRaw("Mouse X");
        //float mouseY = Input.GetAxisRaw("Mouse Y");
        
        Vector2 moveVector = new Vector2(hori * MoveSpeed, vert * MoveSpeed);
        //float angle = math.cos(mouseY/mouseX);

        rigBod.linearVelocity = moveVector;
        //rigBod.SetRotation(angle);
    }
}
