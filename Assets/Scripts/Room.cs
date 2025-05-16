using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // set a list to reference the doors
    public List<GameObject> doors = new List<GameObject>();

    public void OpenDoors(bool[] status)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            // open doors if false, close if true
            doors[i].SetActive(status[i]);
        }
    }
}
