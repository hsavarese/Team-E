using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float health = 0;
    [SerializeField] private float healthMult = 0;
    [SerializeField] private float damage = 0;
    [SerializeField] private float damageMult = 0;
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private float shotsPerSec = 0;
    [SerializeField] private float spsMult = 0;
    [SerializeField] private float accuracy = 0;
    [SerializeField] private float shotDistance = 0;
    [SerializeField] private float moveSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pickupItem(PlayerStats stats){
        if(health !=0 || healthMult != 0)
            stats.updateHealth(health, healthMult);

        if(damage != 0 || damageMult != 0)
            stats.updateDamage(damage, damageMult);

        if(bulletSpeed != 0)
            stats.updateShotSpeed(bulletSpeed);

        if(shotsPerSec != 0 || spsMult != 0)
            stats.updateShotsPerSec(shotsPerSec, spsMult);

        if(accuracy != 0)
            stats.updateAccuracy(accuracy);

        if(shotDistance != 0)
            stats.updateShotDistance(shotDistance);

        if(moveSpeed != 0)
            stats.updateMovement(moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            pickupItem(stats);

            Destroy(this.gameObject);
        }
    }
}
