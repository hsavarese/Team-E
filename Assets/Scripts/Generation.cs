using System;
using UnityEngine;
using System.Collections;

public class Generation : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    private GameObject lastRoom;
    public int numberOfRooms = 5;
    int[,] grid = new int[100, 100];
    public float debgugWaitTime = 0.0f;

    private bool first = true;

    void Start()
    {
        StartCoroutine(GenerateRooms());  // Start the coroutine
    }

    public IEnumerator GenerateRooms()
    {
        //Places starting room
        GameObject prevRoom = Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.identity);
        Room prevRoomScript = prevRoom.GetComponent<Room>();

        int direction = 0;

        int roomPointerRow = 50;
        int roomPointerCol = 50;
        grid[50, 50] = 1; // Mark the starting point as filled

        for (int i = 1; i < numberOfRooms + 1; i++)
        {
            float rand = UnityEngine.Random.value;

            //Chooses a random weighted direction 
            if (rand < 0.3f)
                direction = 1;
            else if (rand < 0.6f)
                direction = 2;
            else if (rand < 0.8f)
                direction = 3;
            else
                direction = 4;

            // If the direction is already taken, try to find a valid one
            if (CheckDirection(direction, roomPointerRow, roomPointerCol) != 0)
            {
                bool foundDir = false;
                for (int j = 1; j < 5; j++)
                {
                    if (CheckDirection(j, roomPointerRow, roomPointerCol) == 0)
                    {
                        direction = j;
                        foundDir = true;
                        break;
                    }

                }
                if (!foundDir)
                {
                    yield break;
                }

            }


            GameObject newRoom;
            if (i == numberOfRooms)
            {
                newRoom = Instantiate(roomPrefabs[3]);
            }
            else
            {
                newRoom = Instantiate(roomPrefabs[UnityEngine.Random.Range(1, 3)]);
            }

            Room newRoomScript = newRoom.GetComponent<Room>();
            if (first == true)
            {
                direction = 1;
                first = false;
            }
            // Position the room based on the chosen direction and offset of the last room.
            switch (direction)
            {
                case 1: // North
                    if (roomPointerRow > 0 && grid[roomPointerRow - 1, roomPointerCol] == 0)
                    {
                        roomPointerRow--;
                        Vector3 offset = prevRoomScript.NorthEntrance.position - newRoomScript.SouthEntrance.position;
                        newRoom.transform.position += offset;
                        grid[roomPointerRow, roomPointerCol] = 1;

                        prevRoom = newRoom;
                        prevRoomScript = newRoomScript;
                    }
                    break;

                case 2: // East
                    if (roomPointerCol < 99 && grid[roomPointerRow, roomPointerCol + 1] == 0)
                    {
                        roomPointerCol++;
                        Vector3 offset = prevRoomScript.EastEntrance.position - newRoomScript.WestEntrance.position;
                        newRoom.transform.position += offset;
                        grid[roomPointerRow, roomPointerCol] = 1;

                        prevRoom = newRoom;
                        prevRoomScript = newRoomScript;
                    }
                    break;

                case 3: // South
                    if (roomPointerRow < 99 && grid[roomPointerRow + 1, roomPointerCol] == 0)
                    {
                        roomPointerRow++;
                        Vector3 offset = prevRoomScript.SouthEntrance.position - newRoomScript.NorthEntrance.position;
                        newRoom.transform.position += offset;
                        grid[roomPointerRow, roomPointerCol] = 1;

                        prevRoom = newRoom;
                        prevRoomScript = newRoomScript;
                    }
                    break;

                case 4: // West
                    if (roomPointerCol > 0 && grid[roomPointerRow, roomPointerCol - 1] == 0)
                    {
                        roomPointerCol--;
                        Vector3 offset = prevRoomScript.WestEntrance.position - newRoomScript.EastEntrance.position;
                        newRoom.transform.position += offset;
                        grid[roomPointerRow, roomPointerCol] = 1;

                        prevRoom = newRoom;
                        prevRoomScript = newRoomScript;
                    }
                    break;
            }

            //This is to see the generation of the rooms in real time to debug
            yield return new WaitForSeconds(debgugWaitTime);
        }
    }

    // Check if a direction is empty
    int CheckDirection(int direction, int roomPointerRow, int roomPointerCol)
    {
        switch (direction)
        {
            case 1: return grid[roomPointerRow - 1, roomPointerCol]; // North
            case 2: return grid[roomPointerRow, roomPointerCol + 1]; // East
            case 3: return grid[roomPointerRow + 1, roomPointerCol]; // South
            case 4: return grid[roomPointerRow, roomPointerCol - 1]; // West
            default: return -1; // Invalid direction
        }
    }
}
