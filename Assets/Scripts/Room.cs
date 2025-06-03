using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // set a list to reference the doors
    // 0 = up = +z, 1 = right +x, 2 = down = -z, 3 = left = -x
    public List<GameObject> doors = new List<GameObject>();

    public void OpenDoors(bool[] status)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            // open doors if false, close if true
            if (doors[i] != null)
            {
                doors[i].SetActive(status[i]);
            }
        }
    }
}
