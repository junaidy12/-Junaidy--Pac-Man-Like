using UnityEngine;
public class ChaseState : BaseState
{
    public void Enter(Enemy enemy)
    {
    }

    public void FixedUpdate(Enemy enemy)
    {
    }

    public void Update(Enemy enemy)
    {
        if(enemy.player != null)
        {
            //setting the enemy NavMesh Destination to Player Position
            enemy.Agent.destination = enemy.player.transform.position;
            //Check if enemy distance and the player distance is more than the chasing distance threshold
            if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.chasingDistance)
            {
                //switch to patrol state
                enemy.SwitchState(enemy.PatrolState);
            }
        }

    }

    public void Exit(Enemy enemy)
    {
    }
}
