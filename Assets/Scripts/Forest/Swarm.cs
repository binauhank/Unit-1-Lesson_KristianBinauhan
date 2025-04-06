using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swarm : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;
    [SerializeField]
    private int WAYPOINT_THRESHOLD = 1;

    private int waypointIndex;
    private NavMeshAgent agent;
    private Bot bot;
    private bool hiveNotPickedUp = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bot = GetComponent<Bot>();

        HivePickUp.HivePickedUp += OnHivePickedUp;

        agent.SetDestination(waypoints[0].transform.position);
    }

    private void OnHivePickedUp()
    {
        hiveNotPickedUp = false;
    }

    public void Patrol()
    {
        // if close enough to waypoint, then advance index
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) < WAYPOINT_THRESHOLD)
        {
            waypointIndex++;

            // wrap index around to 0 if we reach the end of list
            if (waypointIndex == waypoints.Count)
            {
                waypointIndex = 0;
            }

            agent.SetDestination(waypoints[waypointIndex].transform.position);
        }
    }

    void Update()
    {
        if (hiveNotPickedUp)
        {
            Patrol();
        }
        else
        {
            bot.Pursue();
        }
    }
}
