using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum DroneType { Stationary, Mobile};
    public DroneType _droneType = DroneType.Stationary;

    public DroneType EdroneType { get { return _droneType; } }

    public float rotation = 1f;
    public float movevementSpeed = 5.0f;

    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;

    /// <summary>
    /// This organizes the index of the waypoints
    /// </summary>
    private void Start()
    {
        targetWaypoint = waypoints[targetWaypointIndex];
        lastWaypointIndex = waypoints.Count - 1;
    }

    void Update()
    {
        StationaryRotate();
        MobileMove();
    }

    /// <summary>
    /// This is how the mobile drone works.
    /// </summary>
    void MobileMove()
    {
        if (_droneType == DroneType.Mobile)
        {
            float movementStep = movevementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
            float distance = Vector3.Distance(transform.position, targetWaypoint.position);
            CheckDistanceToWaypoint(distance);
        }
    }

    /// <summary>
    /// This is how the stationary drone rotates.
    /// </summary>
    void StationaryRotate()
    {
        if (_droneType == DroneType.Stationary)
        {
            transform.Rotate(0, rotation, 0 * Time.deltaTime);
        }
    }

    /// <summary>
    /// This makes sure that the drone arrives on the waypoint.
    /// </summary>
    /// <param name="currentDistance"></param>
    void CheckDistanceToWaypoint(float currentDistance)
    {
        if(currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    /// <summary>
    /// This sets the next waypoint that the drone needs to fly to.
    /// </summary>
    void UpdateTargetWaypoint()
    {
        if(targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        targetWaypoint = waypoints[targetWaypointIndex];
    }
}
