using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public int width = 20;  // Grid width
    public int height = 20; // Grid height
    public int walkLength = 100; // Number of steps
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    private HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

    void Start()
    {
        GenerateDungeon();
        SpawnTiles();
    }

    void GenerateDungeon()
    {
        Vector2Int position = new Vector2Int(width / 2, height / 2); // Start in the middle

        for (int i = 0; i < walkLength; i++)
        {
            floorPositions.Add(position); // Mark this tile as floor

            // Randomly move in one of four directions
            Vector2Int direction = GetRandomDirection();
            position += direction;

            // Keep within bounds
            position.x = Mathf.Clamp(position.x, 1, width - 2);
            position.y = Mathf.Clamp(position.y, 1, height - 2);
        }
    }

    void SpawnTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                GameObject tile = floorPositions.Contains(pos) ? floorPrefab : wallPrefab;
                Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    Vector2Int GetRandomDirection()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        return directions[Random.Range(0, directions.Length)];
    }
}
