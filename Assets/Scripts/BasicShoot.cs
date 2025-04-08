using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public GameObject bullet;
    public float speedLeft;
    public float speedRight;
    public float damageLeft;
    public float damageRight;
    public float lifeTimeLeft;
    public float lifeTimeRight;
    public float cooldownLeft;
    public float cooldownRight;
    public float accuracyLeft;
    public float accuracyRight;

    private float cooldownTimer;
    
    void createBullet(float speed, float damage, float lifeTime, float accuracy, Color color){
        Quaternion rotationOffset = transform.rotation;
        rotationOffset.z += UnityEngine.Random.Range(-1, 1) * accuracy;
        GameObject newBullet = Instantiate(bullet, transform.position, rotationOffset);
        BulletMovement bulMov = newBullet.GetComponent<BulletMovement>();
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
        if(cooldownTimer > 0){
            cooldownTimer -= Time.deltaTime;
        }
        bool fireLeft = Input.GetMouseButton(0); //left mouse button
        bool fireRight = Input.GetMouseButton(1); //right mouse button
        if(fireLeft && cooldownTimer <= 0){
            createBullet(speedLeft, damageLeft, lifeTimeLeft, accuracyLeft, Color.red);
            cooldownTimer = cooldownLeft;
        }
        if(fireRight && cooldownTimer <= 0){
            createBullet(speedRight, damageRight, lifeTimeRight, accuracyRight, Color.blue);
            cooldownTimer = cooldownRight;
        }
    }

    
}
