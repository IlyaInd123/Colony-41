using System.Collections.Generic;
using UnityEngine;

public class WaypointViewer : MonoBehaviour
{
    List<Transform> waypoints = new();

    public void UpdateWaypoints()
    {
        waypoints.Clear();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
        waypoints.RemoveAll(child => child == null);
    }

    private void OnDrawGizmos()
    {
        UpdateWaypoints();
        if (waypoints.Count > 0)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(waypoints[i].position, 0.5f);
                if (i < waypoints.Count - 1)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}