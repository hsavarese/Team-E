using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Collider2D teleporter;
    public string sceneToLoad = "Dungeon";

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) // <-- 2D version!
    {
        BioBitPickup[] bioBits = FindObjectsByType<BioBitPickup>(FindObjectsSortMode.None);
        foreach (BioBitPickup bioBit in bioBits)
        {
            Destroy(bioBit.gameObject);
        }
        Debug.Log("Loaded Dungeon");
        SceneManager.LoadScene(sceneToLoad);
        SceneManager.UnloadSceneAsync("TEST_AREA_Anthony");
    }
}
