using System.Collections.Generic;
using UnityEngine;

public class WaypointViewer : MonoBehaviour
{
    List<Transform> waypoints = new();

    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            if (waypoints.Contains(child)) { continue; }
            waypoints.Add(child);
        }
        waypoints.RemoveAll(child => child == null);
    }

    private void OnDrawGizmos()
    {
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