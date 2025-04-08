using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy; 
    private float timer = 0;
    [SerializeField] public float interval = 2;

    void Start()
    {
       
    }
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=interval){
             Instantiate(enemy, transform.position, Quaternion.identity); 
        timer=0;
        }

    }
}   