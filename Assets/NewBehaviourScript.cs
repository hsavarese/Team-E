using UnityEngine;

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
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        rigBod.linearVelocity = new Vector2(hori * MoveSpeed, vert * MoveSpeed);
    }
}
