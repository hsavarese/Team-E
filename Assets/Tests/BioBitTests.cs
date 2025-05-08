using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.TestTools;

public class BioBitTests
{
    private GameObject bioBitManagerGO;
    private BioBitManager bioBitManager;
    private Text bioBitText;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        bioBitManagerGO = new GameObject("BioBitManagerTest");
        bioBitManager = bioBitManagerGO.AddComponent<BioBitManager>();
        bioBitText = new GameObject("BioBitText").AddComponent<Text>();
        bioBitManager.bioBitText = bioBitText;
        PlayerPrefs.DeleteKey("BioBits");
        PlayerPrefs.Save();
        yield return null;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(bioBitText.gameObject);
        Object.DestroyImmediate(bioBitManagerGO);
        PlayerPrefs.DeleteKey("BioBits");
        PlayerPrefs.Save();
    }

    [UnityTest]
    public IEnumerator BioBitManager_Singleton_OnlyOneInstanceExists()
    {
        GameObject secondGO = new GameObject("SecondBioBitManager");
        BioBitManager secondManager = secondGO.AddComponent<BioBitManager>();
        Assert.AreEqual(bioBitManager, BioBitManager.Instance);
        yield return null;
        Assert.IsTrue(secondManager == null);
    }

    [Test]
    public void BioBitManager_AddBits_IncreasesCount()
    {
        int initialBits = bioBitManager.bioBits;
        int amountToAdd = 5;
        bioBitManager.AddBits(amountToAdd);
        Assert.AreEqual(initialBits + amountToAdd, bioBitManager.bioBits);
    }

    [Test]
    public void BioBitManager_AddBits_UpdatesUI()
    {
        int amountToAdd = 5;
        bioBitManager.AddBits(amountToAdd);
        Assert.AreEqual("BIO Bits: " + bioBitManager.bioBits.ToString(), bioBitText.text);
    }

    [Test]
    public void BioBitManager_Persistence_SaveAndLoad()
    {
        int amountToAdd = 10;
        bioBitManager.AddBits(amountToAdd);
        Object.DestroyImmediate(bioBitManagerGO);
        bioBitManagerGO = new GameObject("BioBitManagerTestReload");
        bioBitManager = bioBitManagerGO.AddComponent<BioBitManager>();
        bioBitManager.bioBitText = bioBitText;
        Assert.AreEqual(amountToAdd, bioBitManager.bioBits);
    }

    [UnityTest]
    public IEnumerator BioBitPickup_OnTriggerEnter2D_WithPlayer_AddsBitsAndDestroys()
    {
        GameObject pickupGO = new GameObject("BioBitPickup");
        BioBitPickup pickup = pickupGO.AddComponent<BioBitPickup>();
        pickup.value = 3;
        GameObject playerGO = new GameObject("Player");
        playerGO.tag = "Player";
        Collider2D playerCollider = playerGO.AddComponent<BoxCollider2D>();
        pickupGO.AddComponent<BoxCollider2D>();
        pickup.OnTriggerEnter2D(playerCollider);
        yield return null;
        Assert.AreEqual(3, bioBitManager.bioBits);
        Assert.IsTrue(pickup == null || pickupGO == null);
    }

    [Test]
    public void BioBitPickup_OnTriggerEnter2D_WithNonPlayer_DoesNothing()
    {
        GameObject pickupGO = new GameObject("BioBitPickup");
        BioBitPickup pickup = pickupGO.AddComponent<BioBitPickup>();
        pickup.value = 3;
        GameObject nonPlayerGO = new GameObject("NonPlayer");
        nonPlayerGO.tag = "Untagged";
        Collider2D nonPlayerCollider = nonPlayerGO.AddComponent<BoxCollider2D>();
        pickupGO.AddComponent<BoxCollider2D>();
        pickup.OnTriggerEnter2D(nonPlayerCollider);
        Assert.AreEqual(0, bioBitManager.bioBits);
        Assert.IsNotNull(pickupGO);
    }
} 