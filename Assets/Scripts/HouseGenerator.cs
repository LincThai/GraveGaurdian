using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    // editable values
    [Header("Editable Values")]
    public int floorHeight = 3;
    public int roomSize = 3;
    public int numOfSlots = 4;

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

    GameObject[,,] tower;

    bool hasStairs = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // random number generation
        numOfFloors = Random.Range(1, 4);
        tower = new GameObject[numOfSlots, numOfSlots, numOfFloors];
        //floorType = Random.Range(0, floorTypes.Count);
        // make an array to store the number of room per floor
        // with the length of the arrey equal to the number of floors
        int[] floorRooms = new int[numOfFloors];

        // integer for assigning values to array
        int n = 0;
        do
        {
            // assign a random numbers of rooms to each element of the array
            numOfRooms = Random.Range(1, 17);
            floorRooms[n] = numOfRooms;
            n++;
        } while (n < numOfFloors);

        
        for (int i = 0; i < floorRooms.Length; i++)
        {
            // spawn a floor every 3m then add to a list to later be destroyed
            GameObject currentFloor = Instantiate(floor,
                new Vector3(0, i * floorHeight, 0), Quaternion.identity, transform);
            currentHouseComp.Add(currentFloor);

            // spawn stairs
            if (i + 1 < numOfFloors)
            {
                GameObject stairs = Instantiate(, new Vector3(0, 0, 0), Quaternion.identity, currentFloor.transform);
                // set position
                stairs.transform.localPosition = new Vector3(1.5f, 0, -1.5f);
                // check if odd/even
                if (i % 2 == 0)
                {
                    stairs.transform.localPosition = new Vector3(3.5f, 0, -1.5f);
                }
            }
            // open whole
            if(i-1 >= 0)
            {
                // deactivate the whole for the staircase
                currentFloor.transform.Find("StairWhole").gameObject.SetActive(false);
            }

            for (int j = 0; j < floorRooms[i]; j++)
            {
                int x = j % numOfSlots;
                int y = Mathf.FloorToInt(j / numOfSlots);
                // spawn room as child of the floor then change transform.localPosition then
                // add to the list to later be destroyed 
                GameObject room = Instantiate(roomTypes[Random.Range(0, roomTypes.Count)],
                    new Vector3(0, 0, 0), Quaternion.identity, currentFloor.transform);

                tower[x, y, i] = room;
                // calculate the position and move
                room.transform.localPosition = new Vector3(x * roomSize, 0, y * -roomSize);
            }
        }

        // spawns roof
        GameObject roof = Instantiate(ceiling,
            new Vector3(0, numOfFloors * floorHeight, 0), Quaternion.identity, transform);
        // adds roof to list
        currentHouseComp.Add(roof);
    }

    // Update is called once per frame
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
