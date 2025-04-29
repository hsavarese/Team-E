using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenWorldGeneration : MonoBehaviour

{
    public GameObject[] roomTiles;
    public int widthOfOpenWorld = 20;
    public int heightOfopenWorld = 20;
    private int roomSize = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject firstTile = Instantiate(roomTiles[4]);
        firstTile.transform.position -= new UnityEngine.Vector3(roomSize * heightOfopenWorld / 2, roomSize * widthOfOpenWorld / 2, 0f);
        for (int i = 0; i < heightOfopenWorld; i++)
        {
            for (int j = 0; j < widthOfOpenWorld; j++)
            {
                GameObject newTile = Instantiate(roomTiles[GetWeightedRandom()]);

                UnityEngine.Vector3 offset = firstTile.transform.position + new UnityEngine.Vector3(roomSize * i, roomSize * j, 0f);

                newTile.transform.position += offset;


            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    int GetWeightedRandom()
    {
        float rand = UnityEngine.Random.value; // Random float between 0.0 and 1.0

        if (rand < 0.05f)
            return 0;
        else if (rand < 0.10f)
            return 1;
        else if (rand < 0.15f)
            return 2;
        else if (rand < 0.30f)
            return 3;
        else if (rand < 0.45f)
            return 4;
        else if (rand < 0.60f)
            return 5;
        else if (rand < 0.75f)
            return 6;
        else if (rand < 0.95f)
            return 7;
        else // 0.90 - 1.00 range
            return 8;
    }
}
