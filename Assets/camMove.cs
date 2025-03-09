using UnityEngine;
using UnityEngine.UIElements;

public class camMove : MonoBehaviour
{
    private Rigidbody2D player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position;
        transform.position += Vector3.back * 10;
    }
}
