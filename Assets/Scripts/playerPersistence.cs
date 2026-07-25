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
       

        SpawnID[] spawns = FindObjectsOfType<SpawnID>();

        foreach (SpawnID spawn in spawns)
        {
           

            if (spawn.SpawnID_ == SpawnManager.NextSpawnID)
            {
                transform.position = spawn.transform.position;
                
                break;
            }
        }
    }
}