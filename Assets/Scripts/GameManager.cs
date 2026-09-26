using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        if (scene.name == "MainGame")
        {
            GameObject unraveler = GameObject.Find("TheUnraveler");

            if (unraveler != null)
            {
                unraveler.SetActive(true);
                Debug.Log("The Unraveler is ACTIVE.");
            }
            else
            {
                Debug.LogError("The Unraveler was NOT found in MainGame!");
            }
        }
        else
        {
            Debug.Log("The Unraveler is OFF in " + scene.name);
        }
    }
}