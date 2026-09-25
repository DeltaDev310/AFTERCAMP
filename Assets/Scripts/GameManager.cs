using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject TheUnraveler;

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
        UpdateUnravelerState(SceneManager.GetActiveScene());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUnravelerState(scene);
    }

    private void UpdateUnravelerState(Scene scene)
    {
        if (TheUnraveler == null)
            return;

        bool inMainGame = scene.name == "MainGame";

        Debug.Log(
            "SCENE: " + scene.name +
            " | UNRAVELER: " + inMainGame
        );

        TheUnraveler.SetActive(inMainGame);
    }
}