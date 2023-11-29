using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Player player;
    [field: SerializeField] public float chasingDistance { get; private set; } = 5f;

    [HideInInspector]
    public NavMeshAgent NavMeshAgent;

    BaseState currentState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    public List<Transform> Waypoints = new List<Transform>();

    private void Awake()
    {
        currentState = PatrolState;
        currentState.Enter(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player.OnPowerStarted += () => SwitchState(RetreatState);        
        player.OnPowerStoped += () => SwitchState(PatrolState);        
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.Update(this);
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdate(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        //exit the previous state
        if(currentState != null)
        {
            currentState.Exit(this);
        }
        //set the currentState to the desire state
        currentState = state;
        //enter the currentState
        currentState.Enter(this);
        Debug.Log("Current state : " + currentState.ToString());
    }
}

