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
    private GameObject deathScreenInstance;

    [SetUp]
    public void SetUp()
    {
        deathScreenInstance = new GameObject();
        deathScreenInstance.AddComponent<FakeDeathScreen>(); // use FakeDeathScreen (inherits from DeathScreen)
    }

    [TearDown]
    public void TearDown()
    {
        if (deathScreenInstance != null)
        {
            UnityEngine.Object.DestroyImmediate(deathScreenInstance);
        }
    }

    /*
    Check to make sure the Health component's start function works properly
    */
    [UnityTest]
    [TestCase(10, ExpectedResult = null)] 
    [TestCase(0, ExpectedResult = null)] 
    [TestCase(-10, ExpectedResult = null)] 
    [TestCase(0.5f, ExpectedResult = null)] 
    public IEnumerator HealthInstantiatesProperly(float maxHealth)
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();

        hp.maxHealthPoints = maxHealth;
        hp.iFrames = 0;

        yield return null;

        Assert.AreEqual(hp.getCurrentHealth(), hp.maxHealthPoints);
        Assert.AreEqual(hp.getCurrentHealth(), Mathf.Max(maxHealth, 1));
    }

    /*
    Checks that the DealDamage function reduces the healthPoints by the inputted amount
    Should only deal positive damage
    */
    [UnityTest]
    [TestCase(10, ExpectedResult = null)] 
    [TestCase(0, ExpectedResult = null)] 
    [TestCase(-10, ExpectedResult = null)] 
    public IEnumerator HealthDealDamage(float damage)
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damageDealt = hp.DealDamage(damage);

        Assert.AreEqual(Mathf.Max(damage, 0), damageDealt);
        Assert.AreEqual(hp.getCurrentHealth(), 100 - damageDealt);
    }

    /*
    Checks that dealDamage can only deal damage once every iFrame seconds
    Zero damage should still trigger invulnerability
    */
    [UnityTest]
    [TestCase(10, ExpectedResult = null)] 
    [TestCase(0, ExpectedResult = null)] 
    [TestCase(-10, ExpectedResult = null)] 
    public IEnumerator HealthInvulnerable(float damage)
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

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
    public IEnumerator HealthNoIFrames()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damageDealt = hp.DealDamage(1);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

    /*
    Checks that invulnerability lasts at least until the next frame
    */
    [UnityTest]
    public IEnumerator HealthIFramesLastMoreThanAFrame()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

        float damageDealt = hp.DealDamage(1);

        yield return null;

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(hp.getCurrentHealth(), 99);
    }

    /*
    Checks that invulnerability ends after iFrame seconds
    */
    [UnityTest]
    public IEnumerator HealthIFramesEnd()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames);

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

    /*
    Checks that invulnerability lasts at least 98% of the intended length
    */
    [UnityTest]
    public IEnumerator HealthIFramesTimedCorrectly()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames * 0.98f);

        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(hp.getCurrentHealth(), 99);
    }

    /*
    Checks that dealDamage doesn't reset the invulnerability timer while invulnerable
    */
    [UnityTest]
    public IEnumerator HealthIFramesDontReset()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

        float damageDealt = hp.DealDamage(1);

        yield return new WaitForSeconds(hp.iFrames / 2);

        float damageDealtInvulnerable = hp.DealDamage(2);

        yield return new WaitForSeconds(hp.iFrames / 2);

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
    [TestCase(200, ExpectedResult = null)] 
    [TestCase(10, ExpectedResult = null)] 
    [TestCase(0, ExpectedResult = null)] 
    [TestCase(-10, ExpectedResult = null)] 
    public IEnumerator HealthRestoreHealth(float healAmount)
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damage = 12;
        hp.DealDamage(damage);
        float healthRestored = hp.RestoreHealth(healAmount);

        Assert.AreEqual(healthRestored, MathF.Min(damage, MathF.Max(0, healAmount)));
        Assert.AreEqual(hp.getCurrentHealth(), (100 - damage) + healthRestored);
        Assert.GreaterOrEqual(hp.maxHealthPoints, hp.getCurrentHealth());
    }

    /*
    Checks that if health is reduced to 0 or less, it triggers death
    */
    [UnityTest]
    [TestCase(0, ExpectedResult = null)]
    [TestCase(10, ExpectedResult = null)]
    public IEnumerator HealthDeath(float extraDamage)
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();

        hp.deathScreen = deathScreenInstance.GetComponent<DeathScreen>(); // Correct assignment

        hp.maxHealthPoints = 100;
        hp.iFrames = 0;
        hp.isEnemy = false;

        yield return null;

        hp.DealDamage(hp.maxHealthPoints + extraDamage);

        yield return null;
        yield return null;

        Assert.IsTrue(hp.getCurrentHealth() <= 0);
        Assert.IsTrue(hp.GetHasDied());
    }

    /*
    Checks that getHealthPercent gets the correct health percentage
    Should only return between 0 and 1
    */
    [UnityTest]
    public IEnumerator HealthPercentage()
    {
        GameObject gameOb = new GameObject();
        Health hp = gameOb.AddComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        Assert.AreEqual(hp.getHealthPercent(), 1);

        hp.DealDamage(10);

        Assert.AreEqual(hp.getHealthPercent(), 0.9f);

        hp.DealDamage(90);

        Assert.AreEqual(hp.getHealthPercent(), 0);

        hp.DealDamage(10);

        Assert.AreEqual(hp.getHealthPercent(), 0);
    }
}



