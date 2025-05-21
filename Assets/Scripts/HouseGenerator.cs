using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    // editable values
    [Header("Editable Values")]
    public int floorHeight = 3;
    public int roomSize = 3;
    public int numOfSlots = 4;
    public int maxNumFloors = 5;

    // numbers for random generation
    [Header("Randomly Generated Values")]
    public int numOfFloors;
    public int floorType;
    public int numOfRooms;

    // Storage of gameObjects/prefabs to instantiate
    [Header("List of Generated Objects")]
    public GameObject floor;
    public GameObject ceiling;
    public List<GameObject> roomTypes = new List<GameObject>();

    // hidden variables/data structures
    List<GameObject> currentHouseComp = new List<GameObject>();

    void Start()
    {
        // random number generation
        numOfFloors = Random.Range(1, maxNumFloors);   
        // make an array to store the number of rooms per floor
        // with the length of the arrey equal to the number of floors
        int[] floorRooms = new int[numOfFloors];
        // create a vector 3 to store the x and y position of the bottom half og the stairs
        Vector3 stairPos = Vector3.zero;

        // integer for assigning values to array
        int n = 0;
        do
        {
            // assign a random numbers of rooms to each element of the array
            numOfRooms = Random.Range(1, numOfSlots * numOfSlots);
            floorRooms[n] = numOfRooms;
            n++;
        } while (n < numOfFloors);

        int stairX = -1, stairY = -1;
        for (int i = 0; i < floorRooms.Length; i++)
        {
            // spawn a floor every numeber of meters assigned by floorHeight then add to a list to later be destroyed
            GameObject currentFloor = Instantiate(floor,
                new Vector3(0, i * floorHeight, 0), Quaternion.identity, transform);
            currentHouseComp.Add(currentFloor);

            GameObject[,] rooms = new GameObject[numOfRooms/2,numOfRooms/2];

            if (i == 0)
            {
                // spawn the starting room
                rooms[0, 0] = Instantiate(roomTypes[0], new Vector3(0, 0, 0), Quaternion.identity, currentFloor.transform);
            }

            if (i - 1 >= 0)
            {
                // spawn the stair opening for the stairs
                rooms[stairX, stairY] = Instantiate(roomTypes[2], new Vector3(0, 0, 0), Quaternion.identity , currentFloor.transform);
                // set the position
                rooms[stairX, stairY].transform.localPosition = stairPos;
            }

            if (i + 1 < numOfFloors)
            {
                // this picks a random spot to spawn stairs
                stairX = Random.Range(0, rooms.GetLength(0));
                stairY = Random.Range(0, rooms.GetLength(1));
                // this spawns the stairs
                rooms[stairX, stairY] = Instantiate(roomTypes[1], new Vector3(0, 0, 0), Quaternion.identity, currentFloor.transform);
                // sets the position then save it
                rooms[stairX, stairY].transform.localPosition = new Vector3( stairX * roomSize, 0, stairY * -roomSize);
                stairPos = rooms[stairX, stairY].transform.localPosition;
            }
            
            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for(int y = 0; y < rooms.GetLength(1); y++)
                {
                    // skip over the rooms that are already set
                    if (rooms[x, y] == null)
                    {
                        // spawn a random room type that is not stair components or the start room
                        rooms[x, y] = Instantiate(roomTypes[Random.Range(3, roomTypes.Count)], new Vector3(0, 0, 0), Quaternion.identity, currentFloor.transform);
                        // set the position of each room
                        rooms[x, y].transform.localPosition = new Vector3( x * roomSize, 0, y * -roomSize);
                    }
                }
            }
        }

        // spawns roof
        GameObject roof = Instantiate(ceiling,
            new Vector3(0, numOfFloors * floorHeight, 0), Quaternion.identity, transform);
        // adds roof to list
        currentHouseComp.Add(roof);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHouse();
            Start();
        }
    }

    void ResetHouse()
    {
        // destroy all the game objects spawned
        for (int i = 0; i < currentHouseComp.Count; i++)
        {
            //int x = i % 3;
            //int y = Mathf.FloorToInt(i / 3);

            Destroy(currentHouseComp[i]);
            //Destroy(tower[x, y, i]);
        }
        currentHouseComp.Clear();
    }
}
