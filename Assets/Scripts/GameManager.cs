using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DoorPasscode DoorPasscode;
    public GameObject DoorEntrace;
    public PlayerMovement PlayerMovement;

    [Header("Persistent Objects")]
    public GameObject Player;
    public GameObject TheUnraveler;

    [Header("Main Game Spawn")]
    public Vector2 mainGameSpawn = new Vector2(-51.3162f, 4.34f);

    [Header("Unraveler Spawn")]
    public Vector2 unravelerSpawn = new Vector2(154f, 31f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            Debug.Log("GAMEMANAGER INITIALIZED");
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
        Debug.Log("================================");
        Debug.Log("LOADED SCENE: " + scene.name);
        Debug.Log("================================");

        // =========================
        // MAIN GAME
        // =========================

        if (scene.name == "MainGame")
        {
            // -------------------------
            // PLAYER
            // -------------------------

            if (Player != null)
            {
                Player.SetActive(true);

                Player.transform.position = mainGameSpawn;

                Debug.Log(
                    "PLAYER RESET TO: " +
                    Player.transform.position
                );
            }
            else
            {
                Debug.LogError(
                    "GAME MANAGER: PLAYER REFERENCE IS NULL!"
                );
            }

            // -------------------------
            // UNRAVELER
            // -------------------------

            if (TheUnraveler != null)
            {
                TheUnraveler.SetActive(true);

                TheUnraveler.transform.position =
                    unravelerSpawn;

                Debug.Log(
                    "UNRAVELER FORCED TO: " +
                    unravelerSpawn
                );

                Debug.Log(
                    "UNRAVELER ACTUAL POSITION: " +
                    TheUnraveler.transform.position
                );

                EnemyAI enemyAI =
                    TheUnraveler.GetComponent<EnemyAI>();

                if (enemyAI != null)
                {
                    enemyAI.ResetAI();

                    Debug.Log(
                        "UNRAVELER AI RESET"
                    );
                }
                else
                {
                    Debug.LogError(
                        "GAME MANAGER: " +
                        "EnemyAI COMPONENT NOT FOUND!"
                    );
                }

                Debug.Log(
                    "UNRAVELER FINAL POSITION: " +
                    TheUnraveler.transform.position
                );

                Debug.Log(
                    "UNRAVELER ACTIVATED"
                );
            }
            else
            {
                Debug.LogError(
                    "GAME MANAGER: " +
                    "THE UNRAVELER REFERENCE IS NULL!"
                );
            }
            DoorPasscode.PlayerCamera.gameObject.SetActive(true);
            DoorEntrace.SetActive(true);
            PlayerMovement.enabled = true;
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

            if (Player != null)
            {
                Player.SetActive(true);

                Debug.Log(
                    "PLAYER KEPT ACTIVE - SCENE: " +
                    scene.name
                );
            }
         PlayerMovement.enabled = false;
        }
    }
}