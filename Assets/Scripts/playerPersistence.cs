using System.Data;
using UnityEngine;

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
}
