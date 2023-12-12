using UnityEngine;

public class PatrolState : BaseState
{
    bool isMoving;
    Vector3 destinationPos;

    WaypointData previousWaypoint;
    WaypointData currentWaypoint;
    public void Enter(Enemy enemy)
    {
        enemy.Animator.SetTrigger("Patrol");
        isMoving = false;
    }

    public void FixedUpdate(Enemy enemy)
    {
        //Debug.Log("Fixed Update State");
    }

    public void Update(Enemy enemy)
    {
        //Check Player Distance from this Enemy
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.chasingDistance)
        {
            //Switch to Chase State
            currentWaypoint.ResetOccupied();
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!isMoving)
        {
            SetAgentDestination(enemy);
            isMoving = true;
        }
        else
        {
            //Check if enemy distance less than or equal than the stopping distance threshold
            if(Vector3.Distance(enemy.Agent.destination, enemy.transform.position) <= enemy.Agent.stoppingDistance)
            {
                currentWaypoint.ResetOccupied();    
                isMoving = false;
            }
        }
    }

    private void SetAgentDestination(Enemy enemy)
    {
        //Generate Random Index Waypoint
        int wpIndex = UnityEngine.Random.Range(0, Enemy.WaypointManager.WaypointDataList.Count);
        //store the waypoint data
        currentWaypoint = Enemy.WaypointManager.WaypointDataList[wpIndex];
        //check if other enemy already going towards the waypoint
        if (currentWaypoint.GetOccupied() || currentWaypoint == previousWaypoint)
        {
            Debug.Log($"{enemy.name} Waypoint Ocupied! {currentWaypoint.GetTransform().name}");
            //find a new waypoint
            SetAgentDestination(enemy);
            return;
        }
        //Set The position to the destinationPos Variable
        destinationPos = currentWaypoint.GetTransform().position;
        destinationPos.y = 0;
        //set the waypoint to be occupied so no other enemy can go there
        currentWaypoint.SetOccupied();
        //set previous waypoint to current
        previousWaypoint = currentWaypoint;
        //Apply to enemy NavMesh
        enemy.Agent.destination = destinationPos;
        Debug.Log($"{enemy.name} Goes Toward {currentWaypoint.GetTransform().name}");
    }

    public void Exit(Enemy enemy)
    {
    }
}
