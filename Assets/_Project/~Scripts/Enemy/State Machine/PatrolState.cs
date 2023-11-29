using UnityEngine;

public class PatrolState : BaseState
{
    bool isMoving;
    Vector3 destinationPos;

    public void Enter(Enemy enemy)
    {
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
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!isMoving)
        {
            //Generate Random Index Waypoint
            int wpIndex = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
            //Set The position to the destinationPos Variable
            destinationPos = enemy.Waypoints[wpIndex].position;
            destinationPos.y = 0;
            //Apply to enemy NavMesh
            enemy.Agent.destination = destinationPos;
            isMoving = true;
        }
        else
        {
            //Check if enemy distance less than or equal than the stopping distance threshold
            if(Vector3.Distance(enemy.Agent.destination, enemy.transform.position) <= enemy.Agent.stoppingDistance)
            {
                isMoving = false;
            }
        }
    }

    public void Exit(Enemy enemy)
    {
    }
}
