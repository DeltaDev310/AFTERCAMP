using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform Jumpscare;
    public AudioSource JumpScareSound;

    public float patrolSpeed = 8f;
    public float chaseSpeed = 8f;

    public float detectionRange = 20f;
    public float loseRange = 18f;

    private enum State
    {
        Patrol,
        Chase,
        Return
    }

    private State currentState = State.Patrol;

    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    private Vector3 startPosition;

    private bool dying = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (dying)
            return;

        float distanceToPlayer = Vector3.Distance(
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

    void Patrol(float distanceToPlayer)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolPoints[currentPatrolPoint].position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector3.Distance(
            transform.position,
            patrolPoints[currentPatrolPoint].position
        ) < 0.1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
                currentPatrolPoint = 0;
        }

        if (distanceToPlayer <= detectionRange)
            ChangeState(State.Chase);
    }

    void Chase(float distanceToPlayer)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );

        if (distanceToPlayer > loseRange)
            ChangeState(State.Return);
    }

    void Return(float distanceToPlayer)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            startPosition,
            patrolSpeed * Time.deltaTime
        );

        if (Vector3.Distance(
            transform.position,
            startPosition
        ) < 0.1f)
        {
            ChangeState(State.Patrol);
        }

        if (distanceToPlayer <= detectionRange)
            ChangeState(State.Chase);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dying)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            dying = true;
            StartCoroutine(JumpscareSequence());
        }
    }

    private IEnumerator JumpscareSequence()
    {
        Jumpscare.gameObject.SetActive(true);

        JumpScareSound.Play();

        yield return new WaitForSeconds(2f);

        JumpScareSound.Stop();
        Jumpscare.gameObject.SetActive(false);

        ResetGame();

        SceneManager.LoadScene("Intro");
    }

    private void ResetGame()
    {
        // Reset the code
        if (FullCodeManager.Instance != null)
        {
            FullCodeManager.Instance.fullcode.Clear();
            Destroy(FullCodeManager.Instance.gameObject);
        }

        // Reset collected photos
        if (PhotoProgress.Instance != null)
        {
            PhotoProgress.Instance.collectedPhotos.Clear();
            Destroy(PhotoProgress.Instance.gameObject);
        }

        // Reset persistent player
        if (player != null)
        {
            Destroy(player.gameObject);
        }
    }

    void ChangeState(State newState)
    {
        currentState = newState;
    }
}