using UnityEngine;
using UnityEngine.UIElements;

public class camMove : MonoBehaviour
{
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public GameObject trackedObject;
    private Transform player;
    private Vector3 posOffset = Vector3.back * 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = trackedObject.GetComponent<Transform>();
        posOffset = new Vector3(offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPos = player.position + posOffset;
        transform.position = camPos;
    }
}
