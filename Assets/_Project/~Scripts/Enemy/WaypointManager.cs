using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [HideInInspector] public List<WaypointData> WaypointDataList = new List<WaypointData>();

    private void Awake()
    {
        foreach(Transform t in waypoints)
        {
            WaypointDataList.Add(new WaypointData(t));
        }
    }

}


[System.Serializable]
public class WaypointData
{
    [SerializeField] Transform waypoint;
    [SerializeField] bool occupied;

    public WaypointData(Transform waypoint)
    {
        this.waypoint = waypoint;
        this.occupied = false;
    }

    public Transform GetTransform()
    {
        return waypoint;
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public void SetOccupied()
    {
        occupied = true;
    }
    public void ResetOccupied()
    {
        occupied = false;
    }
}

