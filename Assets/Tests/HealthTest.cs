using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(10, ExpectedResult = null)]
    [TestCase(0, ExpectedResult = null)]
    [TestCase(-10, ExpectedResult = null)]
    public IEnumerator HealthInstantiatesProperly(float maxHealth)
    {
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = maxHealth;
        hp.iFrames = 0;

        yield return null;

        Assert.AreEqual(hp.getCurrentHealth(), hp.maxHealthPoints);
        Assert.AreEqual(hp.getCurrentHealth(), Mathf.Max(maxHealth, 1));
    }

    [UnityTest]
    [TestCase(10, ExpectedResult = null)]
    [TestCase(0, ExpectedResult = null)]
    [TestCase(-10, ExpectedResult = null)]
    public IEnumerator HealthDealDamage(float damage){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damageDealt = hp.DealDamage(damage);

        Assert.AreEqual(Mathf.Max(damage, 0), damageDealt);
        Assert.AreEqual(hp.getCurrentHealth(), 100 - damageDealt);
    }

    [UnityTest]
    public IEnumerator HealthInvulnerable(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 1;

        yield return null;

        float damageDealt = hp.DealDamage(1);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 0);
        Assert.AreEqual(hp.getCurrentHealth(), 99);
    }

    [UnityTest]
    public IEnumerator HealthNoIFrames(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damageDealt = hp.DealDamage(1);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

    [UnityTest]
    public IEnumerator HealthIFramesLastMoreThanAFrame(){
        GameObject gameOb = new GameObject();
        gameOb.AddComponent<Health>();
        Health hp = gameOb.GetComponent<Health>();
        hp.maxHealthPoints = 100;
        hp.iFrames = 0;

        yield return null;

        float damageDealt = hp.DealDamage(1);
        float damageDealtInvulnerable = hp.DealDamage(2);

        Assert.AreEqual(damageDealt, 1);
        Assert.AreEqual(damageDealtInvulnerable, 2);
        Assert.AreEqual(hp.getCurrentHealth(), 97);
    }

}
