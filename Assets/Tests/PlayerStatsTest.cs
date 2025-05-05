using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerStatsTest
{
    const string PathPlayer = "Assets\\Player.prefab";
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathPlayer);

    // A Test behaves as an ordinary method
    [Test]
    public void PlayerStatsTestSimplePasses()
    {
        // Use the Assert class to test conditions
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerStatsInstantiates()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        
        //player.SetActive(true);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        float health = stats.getMaxHealth();
        float damage = stats.getDamage();
        float sps = stats.getShotsPerSec();
        float dist = stats.getShotDistance();
        float shotSpeed = stats.getShotSpeed();
        float move = stats.getMoveSpeed();

        yield return null;

        Health healthComp = player.GetComponent<Health>();
        BasicShoot shoot = player.GetComponent<BasicShoot>();
        BasicMovement movement = player.GetComponent<BasicMovement>();

        Assert.True(stats.enabled);
        Assert.True(player.activeSelf);
        Assert.True(stats.isActiveAndEnabled);

        Assert.AreEqual(health, healthComp.getMaxHealth());
        Assert.AreEqual(damage, shoot.getDamage());
        Assert.AreEqual(sps, 1 / shoot.getCooldown());
        Assert.AreEqual(dist, shoot.getLifetime());
        Assert.AreEqual(shotSpeed, shoot.getSpeed());
        Assert.AreEqual(move, movement.getSpeed());
    }

    [UnityTest]
    [TestCase(-1, -1, -1,  ExpectedResult = null)]
    [TestCase(-1, -1, 2,  ExpectedResult = null)]
    public IEnumerator PlayerStatsSetHealthIgnore(float hp, float mult, float negMult)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        stats.setHealth(10, 3, 0.5f);

        float health = stats.getMaxHealth();
        float newhealth = stats.setHealth(hp, mult, negMult);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(health, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    [TestCase(10,  ExpectedResult = null)]
    [TestCase(0,  ExpectedResult = null)]
    public IEnumerator PlayerStatsSetHealthBase(float hp)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float newhealth = stats.setHealth(newHealth: hp);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(Mathf.Max(hp, 1), newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    // [UnityTest]
    // public IEnumerator PlayerStatsSetHealthMult()
    // {
    //     GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>(PathPlayer);
    //     player.SetActive(true);
    //     PlayerStats stats = player.GetComponent<PlayerStats>();
        
    //     yield return null; //skip one frame to let start funcion work

    //     float health = stats.getMaxHealth();
    //     float newhealth = stats.setHealth(newMult: 2);
    //     float maxHealth = player.GetComponent<Health>().getMaxHealth();

    //     Assert.AreEqual(health * 2, newhealth);
    //     Assert.AreEqual(newhealth, maxHealth);
    // }

    [UnityTest]
    public IEnumerator PlayerStatsSetHealthNegMult()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float health = stats.getMaxHealth();
        float newhealth = stats.setHealth(newNegMult: 0.5f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(health * 0.5, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetHealthFull()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float newhealth = stats.setHealth(10, 4, 0.5f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(20, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetHealthAgain()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        stats.setHealth(10, 4, 0.5f);

        float newhealth = stats.setHealth(20, 12, 0.25f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(60, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }



    [UnityTest]
    [TestCase(-1, -1, -1,  ExpectedResult = null)]
    [TestCase(-1, -1, 2,  ExpectedResult = null)]
    public IEnumerator PlayerStatsSetDamageIgnore(float dam, float mult, float negMult)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        stats.setDamage(10, 3, 0.5f);

        float damage = stats.getDamage();
        float newDamage = stats.setDamage(dam, mult, negMult);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(damage, newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    [UnityTest]
    [TestCase(10,  ExpectedResult = null)]
    [TestCase(0,  ExpectedResult = null)]
    public IEnumerator PlayerStatsSetDamageBase(float dam)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float newDamage = stats.setDamage(newDamage: dam);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(Mathf.Max(dam, 1), newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetDamageMult()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float damage = stats.getDamage();
        float newDamage = stats.setDamage(newMult: 2);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(damage * 2, newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetDamageNegMult()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float damage = stats.getDamage();
        float newDamage = stats.setDamage(newNegMult: 0.5f);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(damage * 0.5, newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetDamageFull()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float newDamage = stats.setDamage(10, 4, 0.5f);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(20, newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetDamageAgain()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        stats.setDamage(10, 4, 0.5f);

        float newDamage = stats.setDamage(20, 12, 0.25f);
        float curDamage = player.GetComponent<BasicShoot>().getDamage();

        Assert.AreEqual(60, newDamage);
        Assert.AreEqual(newDamage, curDamage);
    }

    

    // [UnityTest]
    // [TestCase(-1, -1, -1,  ExpectedResult = null)]
    // [TestCase(-1, -1, 2,  ExpectedResult = null)]
    // public IEnumerator PlayerStatsSetSPSIgnore(float sps, float mult, float negMult)
    // {
    //     GameObject player = Object.Instantiate(playerPrefab);
    //     PlayerStats stats = player.GetComponent<PlayerStats>();
        
    //     yield return null; //skip one frame to let start funcion work

    //     stats.setShotsPerSec(5, 3, 0.5f);

    //     float baseSPS = stats.getShotsPerSec();
    //     float newSPS = stats.setShotsPerSec(sps, mult, negMult);
    //     float cool = player.GetComponent<BasicShoot>().getCooldown();

    //     Assert.AreEqual(baseSPS, newSPS);
    //     Assert.AreEqual(newSPS, 1 / cool);
    // }

    // [UnityTest]
    // [TestCase(10,  ExpectedResult = null)]
    // [TestCase(0,  ExpectedResult = null)]
    // public IEnumerator PlayerStatsSetSPSBase(float sps)
    // {
    //     GameObject player = Object.Instantiate(playerPrefab);
    //     PlayerStats stats = player.GetComponent<PlayerStats>();
        
    //     yield return null; //skip one frame to let start funcion work

    //     float newhealth = stats.setShotsPerSec(newShots: sps);
    //     float maxHealth = player.GetComponent<Health>().getMaxHealth();

    //     Assert.AreEqual(Mathf.Max(sps, 0.1f), newhealth);
    //     Assert.AreEqual(newhealth, maxHealth);
    // }

    [UnityTest]
    public IEnumerator PlayerStatsSetSPSMult()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float health = stats.getMaxHealth();
        float newhealth = stats.setHealth(newMult: 2);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(health * 2, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetSPSNegMult()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float health = stats.getMaxHealth();
        float newhealth = stats.setHealth(newNegMult: 0.5f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(health * 0.5, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetSPSFull()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        float newhealth = stats.setHealth(10, 4, 0.5f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(20, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerStatsSetSPSAgain()
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        
        yield return null; //skip one frame to let start funcion work

        stats.setHealth(10, 4, 0.5f);

        float newhealth = stats.setHealth(20, 12, 0.25f);
        float maxHealth = player.GetComponent<Health>().getMaxHealth();

        Assert.AreEqual(60, newhealth);
        Assert.AreEqual(newhealth, maxHealth);
    }
}
