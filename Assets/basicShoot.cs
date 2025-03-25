using UnityEngine;

public class basicShoot : MonoBehaviour
{
    public GameObject bullet;
    public float speedLeft;
    public float speedRight;
    public float damageLeft;
    public float damageRight;
    public float lifeTimeLeft;
    public float lifeTimeRight;
    
    void createBullet(float speed, float damage, float lifeTime, Color color){
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        bulletMovement bulMov = newBullet.GetComponent<bulletMovement>();
        BullletDamage bulDam = newBullet.GetComponent<BullletDamage>();
        bulMov.speed = speedLeft;
        bulMov.lifeTime = lifeTimeLeft;
        bulDam.damage = damage;
        bulDam.isEnemy = false;
        newBullet.GetComponent<Renderer>().material.color = color;
    }
    
    
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
            createBullet(speedLeft, damageLeft, lifeTimeLeft, Color.red);
        }
        if(fireRight){
            createBullet(speedRight, damageRight, lifeTimeRight, Color.blue);
        }
    }

    
}
