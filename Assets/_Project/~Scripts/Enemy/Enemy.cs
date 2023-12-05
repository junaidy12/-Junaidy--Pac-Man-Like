using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Player player;
    [field: SerializeField] public float chasingDistance { get; private set; } = 5f;
    [SerializeField] float baseSpeed = 2.25f;
    [SerializeField][Range(0.1f,1f)] float retreatSpeedMultiplier = .5f;

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Animator Animator;

    BaseState currentState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    public List<Transform> Waypoints = new List<Transform>();


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        currentState = PatrolState;
        currentState.Enter(this);
    }

    private void Start()
    {
        Agent.speed = baseSpeed;

        player.OnPowerStarted += Player_OnPowerStarted;
        player.OnPowerStoped += Player_OnPowerStopped;
    }

    private void OnDestroy()
    {
        player.OnPowerStarted -= Player_OnPowerStarted;
        player.OnPowerStoped -= Player_OnPowerStopped;
    }

    private void Player_OnPowerStopped()
    {
        Agent.speed = baseSpeed;
        SwitchState(PatrolState);
    }

    private void Player_OnPowerStarted()
    {
        SwitchState(RetreatState);
        Agent.speed *= retreatSpeedMultiplier;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState == RetreatState) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Dead();
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

    public void Dead()
    {
        Destroy(gameObject);
    }
}

