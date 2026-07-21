using UnityEngine;
using UnityEngine.SceneManagement;

public class playerPersistence : MonoBehaviour
{
    private static playerPersistence instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Looking for Spawn ID: " + SpawnManager.NextSpawnID);

        SpawnID[] spawns = FindObjectsOfType<SpawnID>();

        foreach (SpawnID spawn in spawns)
        {
            Debug.Log("Found Spawn ID: " + spawn.SpawnID_);

            if (spawn.SpawnID_ == SpawnManager.NextSpawnID)
            {
                transform.position = spawn.transform.position;
                Debug.Log("Teleported!");
                break;
            }
        }
    }
}