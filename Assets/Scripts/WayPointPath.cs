using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWayPointIndex(int currentWaypointIndex)
    {
        int newtWaypointIndex = currentWaypointIndex + 1;

        if (newtWaypointIndex == transform.childCount)
        {
            newtWaypointIndex = 0;
        }
        return newtWaypointIndex;
    }
}
