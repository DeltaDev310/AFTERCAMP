using TMPro;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player object

    public Transform player;

    public float patrolSpeed = 8f;
    public float chaseSpeed = 8f;

    public float detectionRange = 20f;
    public float loseRange = 18f;

    

    private enum State // Define the states for the enemy AI
    {
        Patrol,
        Chase,
        Return
    }

    private State currentState = State.Patrol; // Start in the Patrol state

    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    private Vector3 startPosition; // Store the starting position of the enemy

    void Start()
    {
        startPosition = transform.position; // Store the starting position of the enemy
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        switch (currentState)// Handle the current state of the enemy AI
        {
            case State.Patrol:
                Patrol(distanceToPlayer);
                break;

            case State.Chase:
                Chase(distanceToPlayer);
                break;

            case State.Return:
                Return(distanceToPlayer);
                break;
        }

    }
    void Patrol(float distanceToPlayer)//  Move between patrol points
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolPoints[currentPatrolPoint].position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector3.Distance(
            transform.position,
            patrolPoints[currentPatrolPoint].position) < 0.1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }

        if (distanceToPlayer <= detectionRange)
        {
            ChangeState(State.Chase);
        }
    }

    void Chase(float distanceToPlayer)// Move towards the player
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
        if (distanceToPlayer > loseRange)
        {
            currentState = State.Return;
        }
    }
    void Return(float distanceToPlayer)// Move back to the starting position
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            startPosition,
            patrolSpeed * Time.deltaTime
        );
        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            currentState = State.Patrol;
        }
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
    }
    
    void ChangeState(State newState)
    {
        currentState = newState;
    }
}
