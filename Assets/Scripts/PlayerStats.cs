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
    [SerializeField] private float accuracy;
    
    //Movement stats
    [SerializeField] private float moveSpeed;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        maxHealthBase = health.setMaxHealth(maxHealthBase, healthMult * healthNegMult);

        shoot = GetComponent<BasicShoot>();
        damageBase = shoot.setDamage(damageBase, damageMult * damageNegMult);
        shotsPerSec = shoot.setShotsPerSec(shotsPerSec);
        shotSpeed = shoot.setBulletSpeed(shotSpeed);
        shotDistance = shoot.setLifetime(shotDistance);
        accuracy = shoot.setAccuracy(accuracy);

        move = GetComponent<BasicMovement>();
        moveSpeed = move.setMoveSpeed(moveSpeed);
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

    //todo
    public float updateAccuracy(float addedAccuracy){
        accuracy = shoot.setAccuracy(addedAccuracy);
        return accuracy;
    }

    public float updateMovement(float addedSpeed){
        moveSpeed = move.setMoveSpeed(moveSpeed + addedSpeed);
        return moveSpeed;
    }
}
