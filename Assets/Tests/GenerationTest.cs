using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class GenerationTests
{
    private GameObject generatorGO;
    private Generation generation;

    [SetUp]
    public void SetUp()
    {
        // Create a Generation object
        generatorGO = new GameObject("GenerationTestObject");
        generation = generatorGO.AddComponent<Generation>();

        // Create dummy room prefabs
        generation.roomPrefabs = new GameObject[4];
        generation.roomPrefabs = new GameObject[4];
        generation.roomPrefabs[0] = Resources.Load<GameObject>("Intersection 1");
        generation.roomPrefabs[1] = Resources.Load<GameObject>("Intersection 1");
        generation.roomPrefabs[2] = Resources.Load<GameObject>("Intersection 1");
        generation.roomPrefabs[3] = Resources.Load<GameObject>("Intersection 1");

        generation.numberOfRooms = 2;
        generation.debgugWaitTime = 0f;

        generatorGO.SetActive(false); // Prevent Start() auto-run
    }



    [UnityTest]
    public IEnumerator GenerateRoomsTestCounT()
    {
        generatorGO.SetActive(true);
        yield return generation.StartCoroutine(generation.GenerateRooms());

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        int expectedRoomCount = generation.numberOfRooms + 1; // +1 for the starting room
        Assert.AreEqual(expectedRoomCount, rooms.Length / 2);
    }
}
