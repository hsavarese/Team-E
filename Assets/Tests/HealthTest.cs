using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*
Tests the Health component

Each Test assumes the ones before it succeed, so if there is an error look at the topmost test that fails
*/
public class HealthTest
{
    /*
    Check to make sure the Health component's start function works propoerly
    */
    [UnityTest]
    [TestCase(10,   ExpectedResult = null)] //positive starting max health
    [TestCase(0,    ExpectedResult = null)] //zero starting max health, should default to 1
    [TestCase(-10,  ExpectedResult = null)] //negative starting max health, should default to 1
    [TestCase(0.5f, ExpectedResult = null)] //starting max health between 0 and 1, should default to 1
    public IEnumerator HealthInstantiatesProperly(float maxHealth)
    {
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = maxHealth;
        hp.iFrames = 0;

        yield return null; //skip one frame

        Assert.AreEqual(hp.getCurrentHealth(), hp.maxHealthPoints);
        Assert.AreEqual(hp.getCurrentHealth(), Mathf.Max(maxHealth, 1)); //check that the minimum starting health is 1
    }

    /*
    Checks that the DealDamage function reduces the healthPoints by the inputed amount
    Should only deal positive damage
    */
    [UnityTest]
    [TestCase(10,  ExpectedResult = null)] //positive damage
    [TestCase(0,   ExpectedResult = null)] //zero damage
    [TestCase(-10, ExpectedResult = null)] //negative damage, should deal 0 damage
    public IEnumerator HealthDealDamage(float damage){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(damage);

        Assert.AreEqual(Mathf.Max(damage, 0), damageDealt); //checks that minimum damage is zero
        Assert.AreEqual(hp.getCurrentHealth(), 100 - damageDealt);
    }

    /*
    Checks that dealDamage can only deal damage once every iFrame seconds
    Zero damage should still trigger invulnerability
    */
    [UnityTest]
    [TestCase(10,  ExpectedResult = null)] //positive damage
    [TestCase(0,   ExpectedResult = null)] //zero damage
    [TestCase(-10, ExpectedResult = null)] //negative damage, should deal 0 damage
    public IEnumerator HealthInvulnerable(float damage){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(damage);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, Mathf.Max(damage, 0));
        Assert.AreEqual(damageDealtInvulnerable, 0); 
        Assert.AreEqual(hp.getCurrentHealth(), 100 - damageDealt);
    }

    /*
    Checks that if iFrames is set to 0, dealDamage can deal multiple instances of damage at once
    */
    [UnityTest]
    public IEnumerator HealthNoIFrames(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(1);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

    /*
    Checks that invulnerability last atleast until the next frame
    */
    [UnityTest]
    public IEnumerator HealthIFramesLastMoreThanAFrame(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(1);

        yield return null; //skip one frame

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(hp.getCurrentHealth(), 99);
    }

    /*
    Checks that invulnerability ends after iFrame seconds
    */
    [UnityTest]
    public IEnumerator HealthIFramesEnd(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames);

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

    /*
    Checks that invulnerability lasts at least 98% of the intended length
    99% was inconsistent
    */
    [UnityTest]
    public IEnumerator HealthIFramesTimedCorrectly(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames * 0.98f); //wait for 98% the length of the IFrames

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(hp.getCurrentHealth(), 99);
    }

    /*
    Checks to make sure that dealDamage doesn't reset the invulnerability timer while invulnerable
    */
    [UnityTest]
    public IEnumerator HealthIFramesDontReset(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null; //skip one frame

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames/2);

        float damageDealtInvulnerable = hp.DealDamage(2);

        yield return new WaitForSeconds(hp.iFrames/2);

        float damageDealtFinal = hp.DealDamage(4);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(damageDealtFinal, 4);
        Assert.AreEqual(hp.getCurrentHealth(), 95);
    }

    /*
    Checks that the RestoreHealth function restores the input amount
    Should only heal positive health
    */
    [UnityTest]
    [TestCase(200, ExpectedResult = null)] //Overheal, should only heal the missing health
    [TestCase(10,  ExpectedResult = null)] //positive heal
    [TestCase(0,   ExpectedResult = null)] //zero heal, should do nothing
    [TestCase(-10, ExpectedResult = null)] //negative heal, should do nothing
    public IEnumerator HealthRestoreHealth(float healAmount){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null; //skip one frame

        float damage = 12;
        hp.DealDamage(damage);
        float healthRestored = hp.RestoreHealth(healAmount);

        Assert.AreEqual(healthRestored, MathF.Min(damage, MathF.Max(0, healAmount))); //checks that the health restored is between the damage dealth and 0
        Assert.AreEqual(hp.getCurrentHealth(), (100 - damage) + healthRestored);
        Assert.GreaterOrEqual(hp.maxHealthPoints, hp.getCurrentHealth()); //checks that health is not above max health
    }

    /*
    Checks that if health is reduced to 0 or less, it destroys itself
    */
    [UnityTest]
    [TestCase(0,  ExpectedResult = null)] //zero extra damage, zero health
    [TestCase(10, ExpectedResult = null)] //positive extra damage, negative health
    public IEnumerator HealthDeath(float extraDamage){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null; //skip one frame

        hp.DealDamage(hp.maxHealthPoints + extraDamage);

        yield return null;//skip two frames because it was inconsistent with only one
        yield return null;

        Assert.True(gameOb == null);
    }

    /*
    Checks that getHealthPercent gets the correct health percentage
    should only return between 0 and 1
    */
    [UnityTest]
    public IEnumerator HealthPercentage(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;//skip one frame

        Assert.AreEqual(hp.getHealthPercent(), 1); //starts at 100% health

        hp.DealDamage(10); // 90 out of 100 health

        Assert.AreEqual(hp.getHealthPercent(), 0.9f);

        hp.DealDamage(90); // 0 out of 100 health

        Assert.AreEqual(hp.getHealthPercent(), 0);

        hp.DealDamage(10); // -10 out of 100 health

        Assert.AreEqual(hp.getHealthPercent(), 0);        
    }
}
