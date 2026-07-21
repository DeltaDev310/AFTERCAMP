using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    public int SpawnID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnManager.NextSpawnID = SpawnID;
            SceneManager.LoadScene(SceneName);
        }
    }
}