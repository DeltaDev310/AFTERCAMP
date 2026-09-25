using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;

    public float detectionRange = 20f;
    public float loseRange = 25f;

    public Transform Jumpscare;
    public AudioSource JumpScareSound;

    private Transform player;
    public GameObject TheUnraveler;

    private int currentPatrolPoint = 0;

    private bool dying = false;

    private enum State
    {
        Patrol,
        Chase,
        Return
    }

    private State currentState = State.Patrol;


    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: Player not found!");
            return;
        }

        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("EnemyAI: No patrol points assigned!");
            return;
        }

        currentPatrolPoint = Random.Range(0, patrolPoints.Length);

        Debug.Log(
            "UNRAVELER STARTED | " +
            "PLAYER: " + player.name +
            " | PATROL POINTS: " + patrolPoints.Length +
            " | STARTING POINT: " + currentPatrolPoint
        );
    }


    private void Update()
    {
        Debug.Log("AI UPDATE");

        if (dying)
            return;

        if (player == null)
            return;

        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        float distanceToPlayer = Vector2.Distance(
            transform.position,
            player.position
        );

        switch (currentState)
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainGame")
        {
            TheUnraveler.SetActive(false);
        }
        if (scene.name == "MainGame")
        {
            TheUnraveler.SetActive(true);
        }
    }

    private void Patrol(float distanceToPlayer)
    {
        if (currentPatrolPoint < 0 || currentPatrolPoint >= patrolPoints.Length)
        {
            Debug.LogError("INVALID PATROL INDEX: " + currentPatrolPoint);
            return;
        }

        Transform target = patrolPoints[currentPatrolPoint];

        if (target == null)
        {
            Debug.LogError(
                "PATROL POINT " + currentPatrolPoint + " IS NULL/DESTROYED"
            );

            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
                currentPatrolPoint = 0;

            return;
        }

        Debug.Log(
            "AI MOVING | Current: " +
            currentPatrolPoint +
            " | Target: " +
            target.name +
            " | Distance: " +
            Vector2.Distance(transform.position, target.position)
        );

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            target.position
        ) < 0.1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
                currentPatrolPoint = 0;
        }

        if (distanceToPlayer <= detectionRange)
        {
            ChangeState(State.Chase);
        }
    }


    private void Chase(float distanceToPlayer)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );

        // Player escaped
        if (distanceToPlayer > loseRange)
        {
            ChangeState(State.Return);
        }
    }


    private void Return(float distanceToPlayer)
    {
        Transform target = patrolPoints[currentPatrolPoint];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            patrolSpeed * Time.deltaTime
        );

        // Reached patrol route
        if (Vector2.Distance(
            transform.position,
            target.position
        ) < 0.1f)
        {
            ChangeState(State.Patrol);
        }

        // Player came back
        if (distanceToPlayer <= detectionRange)
        {
            ChangeState(State.Chase);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dying)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            dying = true;

            if (JumpscareController.Instance != null)
            {
                JumpscareController.Instance.PlayJumpscare();
            }
            else
            {
                Debug.LogError(
                    "EnemyAI: JumpscareController not found!"
                );
            }
        }
    }


    private void ChangeState(State newState)
    {
        currentState = newState;
    }
}