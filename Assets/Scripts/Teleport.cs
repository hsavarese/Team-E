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
        Debug.Log("Loaded Dungoen");
        SceneManager.LoadScene(sceneToLoad);
        SceneManager.UnloadSceneAsync("TEST_AREA_Anthony");


    }
}
