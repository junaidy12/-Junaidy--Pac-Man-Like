﻿using UnityEngine;

public class RetreatState : BaseState 
{
    public void Enter(Enemy enemy)
    {
        enemy.Animator.SetTrigger("Retreat");
    }

    public void FixedUpdate(Enemy enemy)
    {
    }

    public void Update(Enemy enemy)
    {
        if(enemy.player != null)
        {
            //move the enemy away from player
            enemy.Agent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }

    public void Exit(Enemy enemy)
    {
    }
}
