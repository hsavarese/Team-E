using System.Linq.Expressions;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
/*
PlayerStats overrides the variables of the other components
*/
public class PlayerStats : MonoBehaviour
{
    private Health health;
    private BasicShoot shoot;
    private BasicMovement move;

    //Health stats
    [SerializeField] private float maxHealthBase;
    private float healthMult = 1;
    private float healthNegMult = 1;

    //Damage stats
    [SerializeField] private float damageBase;
    private float damageMult = 1;
    private float damageNegMult = 1;
    [SerializeField] private float shotsPerSec;
    private float spsMult = 1;
    private float spsNegMult = 1;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float shotDistance;
    [SerializeField] private float accuracy; //should be int greater than 1
    
    //Movement stats
    [SerializeField] private float moveSpeed;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        maxHealthBase = health.setMaxHealth(maxHealthBase, healthMult * healthNegMult);

        shoot = GetComponent<BasicShoot>();
        if (shoot != null)
        {
            damageBase = shoot.setDamage(damageBase, damageMult * damageNegMult);
            shotsPerSec = shoot.setShotsPerSec(shotsPerSec);
            shotSpeed = shoot.setBulletSpeed(shotSpeed);
            shotDistance = shoot.setLifetime(shotDistance);
            accuracy = shoot.setAccuracy(accuracy);
        }
        else
        {
            Debug.LogWarning("BasicShoot component not found!");
        }

        move = GetComponent<BasicMovement>();
        if (move != null)
        {
            moveSpeed = move.setMoveSpeed(moveSpeed);
        }
        else
        {
            Debug.LogWarning("BasicMovement component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*

    */
    public float updateHealth(float addedHealth, float multIncrease = 0){
        if(multIncrease > 0)
            healthMult += multIncrease;
        else if(multIncrease < 0 && multIncrease > -1)
            healthNegMult *= (-1 * multIncrease);

        maxHealthBase = health.setMaxHealth(maxHealthBase + addedHealth, healthMult * healthNegMult);
        return maxHealthBase * healthMult * healthNegMult;
    }

    public float updateDamage(float addedDamage, float multIncrease = 0){
        if(multIncrease > 0)
            damageMult += multIncrease;
        else if(multIncrease < 0 && multIncrease > -1)
            damageNegMult *= (-1 * multIncrease);

        damageBase = shoot.setDamage(damageBase + addedDamage, damageMult * damageNegMult);
        return damageBase * damageMult * damageNegMult;
    }

    public float updateShotsPerSec(float addedShots, float multIncrease = 0){
        if(multIncrease > 0)
            spsMult += multIncrease;
        else if(multIncrease < 0 && multIncrease > -1)
            spsNegMult *= (-1 * multIncrease);

        shotsPerSec = shoot.setShotsPerSec(shotsPerSec + addedShots, spsMult * spsNegMult);
        return shotsPerSec;
    }

    public float updateShotSpeed(float addedShotSpeed){
        shotSpeed = shoot.setBulletSpeed(shotSpeed + addedShotSpeed);
        return shotSpeed;
    }

    public float updateShotDistance(float addedDistance){
        shotDistance = shoot.setLifetime(shotDistance + addedDistance);
        return shotDistance;
    }

    public float updateAccuracy(float addedAccuracy){
        accuracy = shoot.setAccuracy(accuracy + addedAccuracy);
        return accuracy;
    }

    public float updateMovement(float addedSpeed){
        moveSpeed = move.setMoveSpeed(moveSpeed + addedSpeed);
        return moveSpeed;
    }

    public float getMaxHealth(){
        return maxHealthBase * healthMult * healthNegMult;
    }

    public float getDamage(){
        return damageBase * damageMult * damageNegMult;
    }
    public float getShotsPerSec(){
        return shotsPerSec * spsMult * spsNegMult;
    }
    public float getShotSpeed(){
        return shotSpeed;
    }
    public float getShotDistance(){
        return shotDistance;
    }
    public float getAccuracy(){
        return accuracy;
    }
    
    public float getMoveSpeed(){
        return moveSpeed;
    }

    /*
    sets the players health stats

    if a value is negative it is ignored
    
    returns the new max health
    */
    public float setHealth(float newHealth = -1, float newMult = -1, float newNegMult = -1){
        if(newHealth < 0)
            newHealth = maxHealthBase;
        if(newMult >= 1) 
            healthMult = newMult;
        if(newNegMult > 0 && newNegMult <= 1)
            healthNegMult = newNegMult;
        
        maxHealthBase = health.setMaxHealth(newHealth, healthMult * healthNegMult);
        return maxHealthBase * healthMult * healthNegMult;
    }

/*
    sets the players damage stats

    if any value is negative it is ignored
    if newNegMult greater than 1 it is ignored
    
    returns the new damage dealt per shot
    */
    public float setDamage(float newDamage = -1, float newMult = -1, float newNegMult = -1){
        if(newDamage < 0)
            newDamage = damageBase;
        if(newMult >= 1) 
            damageMult = newMult;
        if(newNegMult > 0 && newNegMult <= 1)
            damageNegMult = newNegMult;

        damageBase = shoot.setDamage(newDamage, damageMult * damageNegMult);
        return damageBase * damageMult * damageNegMult;
    }

    /*
    sets the players shots per second stats

    if a value is negative it is ignored
    
    returns the new shots per second
    */
    public float setShotsPerSec(float newShots = -1, float newMult = -1, float newNegMult = -1){
        if(newShots < 0)
            newShots = shotsPerSec;
        if(newMult >= 1) 
            spsMult = newMult;
        if(newNegMult > 0 && newNegMult <= 1)
            spsNegMult = newNegMult;

        shotsPerSec = shoot.setShotsPerSec(newShots, spsMult * spsNegMult);
        return shotsPerSec * spsMult * spsNegMult;
    }

    public float setShotSpeed(float newShotSpeed){
        shotSpeed = shoot.setBulletSpeed(newShotSpeed);
        return shotSpeed;
    }

    public float setShotDistance(float newDistance){
        shotDistance = shoot.setLifetime(newDistance);
        return shotDistance;
    }

    public float setAccuracy(float newAccuracy){
        accuracy = shoot.setAccuracy(newAccuracy);
        return accuracy;
    }

    public string toString(){
        return  "Health:\t\t\t" + health.getCurrentHealth() + "/" + getMaxHealth() + "\n" + 
                "Damage:\t\t\t" + getDamage() + "\n" +
                "Shots Per Second:\t" + getShotsPerSec() + "\n" +
                "Accuracy:\t\t\t" + getAccuracy() + "\n" +
                "Range:\t\t\t" + getShotDistance() + "\n" +
                "Bullet Speed:\t\t" + getShotDistance() + "\n" +
                "Move Speed:\t\t" + getShotSpeed() + "\n";
                        
    }
}


