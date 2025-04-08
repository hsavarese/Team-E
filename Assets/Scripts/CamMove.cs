using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.1f; //higher number more smooth/delayed it is
    private Vector3 velocity = Vector3.zero;

    private Transform target;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

// Old Camera movment - not smooth  - revert back to if you want, or change smooth time to 0 
// using UnityEngine;
// using UnityEngine.UIElements;

// public class CamMove : MonoBehaviour
// {
//     public float offsetX;
//     public float offsetY;
//     public float offsetZ;
//     public GameObject trackedObject;
//     private Transform player;
//     private Vector3 posOffset = Vector3.back * 10;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         player = trackedObject.GetComponent<Transform>();
//         posOffset = new Vector3(offsetX, offsetY, offsetZ);
//     }

//     // Update is called once per frame
//     void LateUpdate()
//     {
//         Vector3 camPos = player.position + posOffset;
//         transform.position = camPos;
//     }
// }
