using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject Player;
    public GameObject TheUnraveler;

    [Header("Main Game Spawn")]
    public Vector2 mainGameSpawn = new Vector2(-51.3162f, 4.34f);

    [Header("Unraveler Spawn")]
    public Vector2 unravelerSpawn = new Vector2(-0.75f, 1.375f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

    private void Start()
    {
        UpdateSceneState(SceneManager.GetActiveScene());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateSceneState(scene);
    }

    private void UpdateSceneState(Scene scene)
    {
        Debug.Log("LOADED SCENE: " + scene.name);

        // =========================
        // MAIN GAME
        // =========================

        if (scene.name == "MainGame")
        {
            // Activate player
            if (Player != null)
            {
                Player.SetActive(true);

                Player.transform.position = mainGameSpawn;

                Debug.Log(
                    "PLAYER RESET TO: " +
                    mainGameSpawn
                );
            }

            // Activate Unraveler
            if (TheUnraveler != null)
            {
                TheUnraveler.SetActive(true);

                // Reset Unraveler position
                TheUnraveler.transform.position = unravelerSpawn;

                EnemyAI enemyAI =
                    TheUnraveler.GetComponent<EnemyAI>();

                if (enemyAI != null)
                {
                    enemyAI.ResetAI();
                }

                Debug.Log(
                    "UNRAVELER RESET TO: " +
                    unravelerSpawn
                );

                Debug.Log("UNRAVELER ACTIVATED");
            }

        }
        // =========================
         // OTHER SCENES
         // =========================

        else
        {
            if (TheUnraveler != null)
            {
                TheUnraveler.SetActive(false);

                Debug.Log(
                    "UNRAVELER DISABLED - SCENE: " +
                    scene.name
                );
            }
        }
    }
}