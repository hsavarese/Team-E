using UnityEngine;

public class basicShoot : MonoBehaviour
{
    public GameObject bullet;
    public float speedLeft;
    public float speedRight;
    public float lifeTimeLeft;
    public float lifeTimeRight;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool fireLeft = Input.GetMouseButtonDown(0); //left mouse button
        bool fireRight = Input.GetMouseButtonDown(1); //right mouse button
        if(fireLeft){
            GameObject leftBullet = Instantiate(bullet, transform.position, transform.rotation);
            bulletMovement leftBM = leftBullet.GetComponent<bulletMovement>();
            leftBM.speed = speedLeft;
            leftBM.lifeTime = lifeTimeLeft;
            leftBullet.GetComponent<Renderer>().material.color = Color.red;
        }
        if(fireRight){
            GameObject rightBullet = Instantiate(bullet, transform.position, transform.rotation);
            bulletMovement rightBM = rightBullet.GetComponent<bulletMovement>();
            rightBM.speed = speedRight;
            rightBM.lifeTime = lifeTimeRight;
            rightBullet.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
