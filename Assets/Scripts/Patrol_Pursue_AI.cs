using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Pursue_AI : MonoBehaviour {
    enum EnemyState {patrolling, pursuing };

    //public Vector3 boundaryMin;
    //public Vector3 boundaryMax;
    public static Waypoint[] Waypoints;
    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AI;
    private EnemyState enemyState = EnemyState.patrolling;
    public float waypointDistance = 1f;

    void Awake ()
    {
        //if (Waypoints.Length == 0)
        {
            Waypoints = FindObjectsOfType<Waypoint>();
        }
        AI = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        AI.target = GetNearestWaypoint();
	}

    private Transform GetNearestWaypoint()
    {
        Transform nearestWaypoint = Waypoints[0].transform;
        foreach (Waypoint w in Waypoints)
        {
            if ((w.transform.position - transform.position).sqrMagnitude < 
                (nearestWaypoint.position - transform.position).sqrMagnitude)
            {
                nearestWaypoint = w.transform;
            }
        }
        return nearestWaypoint;
    }

    // Update is called once per frame
    void Update ()
    {
        switch (enemyState)
        {
            case EnemyState.patrolling:
                Patrol();
                break;
            case EnemyState.pursuing:
                Pursue();
                break;
        }
        
		
	}

    private void Pursue()
    {
        throw new NotImplementedException();
    }

    private void Patrol()
    {
        if ((transform.position - AI.target.position).magnitude < waypointDistance)
        {
            FindNextWaypoint();
        }
    }

    private void FindNextWaypoint()//Todo: fix this. It currently just grabs one at random 
    {
        AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform;
    }
}
