using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthPoints;
    public float iFrames; //total time invulnerable between attacks
    public bool isEnemy; //true for enemies, flase for allies

    private float invulTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            Destroy(this.gameObject);
            return;
        }
        if(invulTimer > 0){
            invulTimer -= Time.deltaTime;
        }
    }

    //True if they can take damage, Flase If they can't take damage
    public bool IsVulnerable(){
        return invulTimer <= 0;
    }

    /*
    Deals damage to the target reducing the health by the damage if the target can take damage at this time

    Returns the amout of damage delt
    */
    public float DealDamage(float damage){
        if(invulTimer <= 0){
            healthPoints -= damage;
            invulTimer = iFrames;
            return damage;
        } else {
            return 0;
        }
    }
}
