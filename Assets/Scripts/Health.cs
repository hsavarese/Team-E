using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealthPoints; // minimum 1
    public float iFrames; //total time invulnerable between attacks
    public bool isEnemy; //true for enemies, flase for allies

    private float invulTimer;
    private float healthPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(maxHealthPoints >= 1) healthPoints = maxHealthPoints;
        else{
            maxHealthPoints = 1;
            healthPoints = 1;
        } 
    }

    // Update is called once per frame
    void Update() //no matter what damage kills the enemy, the enemy will drop bio bits
    {
        if (healthPoints <= 0)
        {
            if (isEnemy)
            {
                BasicEnemy enemy = GetComponent<BasicEnemy>();
                if (enemy != null)
                {
                    enemy.Die(); 
                    return; // prevent further after destruction
                }
    
            }

            Destroy(this.gameObject); // fallback if no enemy logic
            return;
        }
        if(invulTimer > 0){
            invulTimer -= Time.deltaTime;
        }

    }
    /*{
        if(healthPoints <= 0){
            Destroy(this.gameObject);
            return;
        }
        if(invulTimer > 0){
            invulTimer -= Time.deltaTime;
        }
        
    }*/
    

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
            invulTimer = iFrames;
            if(damage > 0){ 
                healthPoints -= damage;
                return damage;
            }
        }
        return 0;
        
    }

    /*
    Restores health to the target up to their maximum health

    Returns the amout of health restored
    */
    public float RestoreHealth(float heal){
        if(heal <= 0 || healthPoints == maxHealthPoints) return 0; //doesn't heal a negative amount and can't heal past full health

        healthPoints += heal;
        if(healthPoints > maxHealthPoints){
            float amoutHealed = heal - (healthPoints - maxHealthPoints);
            healthPoints = maxHealthPoints;
            return amoutHealed;
        } else return heal;
    }

    /*
    Returns the amount of health remaining as a decimal between 0 and 1;
    */
    public float getHealthPercent(){
        if(healthPoints <= 0) return 0;
        if(healthPoints >= maxHealthPoints) return 1;
        return healthPoints / maxHealthPoints;
    }

    public float getCurrentHealth(){
        return healthPoints;
    }
}
