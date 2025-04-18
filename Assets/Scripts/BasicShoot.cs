using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    const float MIN_DAMAGE = 1;
    const float MIN_LIFE_TIME = 0.1f;
    const float MAX_COOLDOWN = 10;
    [SerializeField] private GameObject bullet;
    private float speedLeft;
    //public float speedRight;
    private float damageLeft;
    //public float damageRight;
    private float lifeTimeLeft;
    //public float lifeTimeRight;
    private float cooldownLeft;
    //public float cooldownRight;
    private float accuracyLeft;
    //public float accuracyRight;

    private float cooldownTimer;
    
    void createBullet(float speed, float damage, float lifeTime, float accuracy, Color color){
        Quaternion rotationOffset = transform.rotation;
        rotationOffset.z += UnityEngine.Random.Range(-1, 1) * accuracy;
        GameObject newBullet = Instantiate(bullet, transform.position, rotationOffset);
        BulletMovement bulMov = newBullet.GetComponent<BulletMovement>();
        BullletDamage bulDam = newBullet.GetComponent<BullletDamage>();
        bulMov.speed = speed;
        bulMov.lifeTime = lifeTime;
        bulDam.damage = damage;
        bulDam.isEnemy = false;
        newBullet.GetComponent<Renderer>().material.color = color;
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        /*
        if(fireRight && cooldownTimer <= 0){
            createBullet(speedRight, damageRight, lifeTimeRight, accuracyRight, Color.blue);
            cooldownTimer = cooldownRight;
        }*/
    }

    

    public float setDamage(float newDamage, float damageMult = 1){
        if(newDamage < MIN_DAMAGE)
            newDamage = MIN_DAMAGE;
        damageLeft = newDamage * damageMult;
        return newDamage;
    }

    public float setShotsPerSec (float shotsPerSec, float spsMult = 1){
        cooldownLeft = 1 / (shotsPerSec * spsMult);
        if(cooldownLeft > MAX_COOLDOWN)
            cooldownLeft = MAX_COOLDOWN;
        return (1 / cooldownLeft) / spsMult;
    }

    public float setBulletSpeed(float newSpeed){
        speedLeft = newSpeed;
        return speedLeft;
    }

    public float setLifetime(float newTime){
        if(newTime < MIN_LIFE_TIME)
            newTime = MIN_LIFE_TIME;
        lifeTimeLeft = newTime;
        return lifeTimeLeft;
    }

    //todo properly define accuracy
    public float setAccuracy(float newAccuracy){
        accuracyLeft = newAccuracy;
        return accuracyLeft;
    }
}
