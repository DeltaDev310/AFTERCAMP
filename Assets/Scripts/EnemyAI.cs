using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;

    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;

    [Header("Detection")]
    public float detectionRange = 20f;
    public float loseRange = 25f;

    private Transform player;

    private int currentPatrolPoint = 0;

    private bool dying = false;

    private enum State
    {
        Patrol,
        Chase,
        Return
    }

    private State currentState = State.Patrol;


    // =========================
    // START
    // =========================

    private void Start()
    {
        FindPlayer();

        FindPatrolPoints();

        Debug.Log(
            "UNRAVELER STARTED | " +
            "Patrol points: " +
            patrolPoints.Length
        );
    }


    // =========================
    // UPDATE
    // =========================

    private void Update()
    {
        if (dying)
            return;

        if (player == null)
        {
            FindPlayer();
            return;
        }

        if (patrolPoints == null ||
            patrolPoints.Length == 0)
        {
            return;
        }

        float distanceToPlayer =
            Vector2.Distance(
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


    // =========================
    // FIND PLAYER
    // =========================

    private void FindPlayer()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;

            Debug.Log(
                "PLAYER FOUND: " +
                player.name
            );
        }
        else
        {
            Debug.LogError(
                "EnemyAI: Player not found!"
            );
        }
    }


    // =========================
    // FIND PATROL POINTS
    // =========================

    private void FindPatrolPoints()
    {
        GameObject[] points =
            GameObject.FindGameObjectsWithTag(
                "PatrolPoint"
            );

        patrolPoints =
            new Transform[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            patrolPoints[i] =
                points[i].transform;
        }

        if (patrolPoints.Length > 0)
        {
            currentPatrolPoint =
                Random.Range(
                    0,
                    patrolPoints.Length
                );
        }

        Debug.Log(
            "PATROL POINTS: " +
            patrolPoints.Length
        );
    }


    // =========================
    // RESET AI
    // =========================

    public void ResetAI()
    {
        Debug.Log("RESETTING UNRAVELER AI");

        dying = false;

        currentState = State.Patrol;

        FindPlayer();

        FindPatrolPoints();

        if (patrolPoints.Length > 0)
        {
            currentPatrolPoint =
                Random.Range(
                    0,
                    patrolPoints.Length
                );
        }

        Debug.Log(
            "UNRAVELER RESET | " +
            "Patrol points: " +
            patrolPoints.Length
        );
    }


    // =========================
    // PATROL
    // =========================

    private void Patrol(float distanceToPlayer)
    {
        if (patrolPoints == null ||
            patrolPoints.Length == 0)
        {
            return;
        }

        Transform target =
            patrolPoints[currentPatrolPoint];

        if (target == null)
        {
            Debug.LogError(
                "PATROL POINT " +
                currentPatrolPoint +
                " IS NULL!"
            );

            FindPatrolPoints();

            return;
        }

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                target.position,
                patrolSpeed *
                Time.deltaTime
            );

        if (Vector2.Distance(
            transform.position,
            target.position
        ) < 0.1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >=
                patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }

        if (distanceToPlayer <=
            detectionRange)
        {
            ChangeState(State.Chase);
        }
    }


    // =========================
    // CHASE
    // =========================

    private void Chase(float distanceToPlayer)
    {
        if (player == null)
            return;

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                player.position,
                chaseSpeed *
                Time.deltaTime
            );

        if (distanceToPlayer >
            loseRange)
        {
            ChangeState(State.Return);
        }
    }


    // =========================
    // RETURN
    // =========================

    private void Return(float distanceToPlayer)
    {
        if (patrolPoints == null ||
            patrolPoints.Length == 0)
        {
            return;
        }

        Transform target =
            patrolPoints[currentPatrolPoint];

        if (target == null)
        {
            FindPatrolPoints();

            return;
        }

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                target.position,
                patrolSpeed *
                Time.deltaTime
            );

        if (Vector2.Distance(
            transform.position,
            target.position
        ) < 0.1f)
        {
            ChangeState(State.Patrol);
        }

        if (distanceToPlayer <=
            detectionRange)
        {
            ChangeState(State.Chase);
        }
    }


    // =========================
    // COLLISION / DEATH
    // =========================

    private void OnCollisionEnter2D(
        Collision2D collision)
    {
        if (dying)
            return;

        if (collision.gameObject.CompareTag(
            "Player"))
        {
            dying = true;

            Debug.Log(
                "UNRAVELER CAUGHT PLAYER"
            );

            if (JumpscareController.Instance != null)
            {
                JumpscareController.Instance
                    .PlayJumpscare();
            }
            else
            {
                Debug.LogError(
                    "EnemyAI: " +
                    "JumpscareController not found!"
                );
            }
        }
    }


    // =========================
    // STATE
    // =========================

    private void ChangeState(State newState)
    {
        currentState = newState;

        Debug.Log(
            "UNRAVELER STATE: " +
            newState
        );
    }
}