using System.Collections.Generic;
using UnityEngine;

public class PhotoProgress : MonoBehaviour
{
    public static PhotoProgress Instance;

    public List<int> collectedPhotos = new List<int>();

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

    public bool HasCollected(int photoNumber)
    {
        return collectedPhotos.Contains(photoNumber);
    }

    public void MarkCollected(int photoNumber)
    {
        if (!collectedPhotos.Contains(photoNumber))
        {
            collectedPhotos.Add(photoNumber);
        }
    }
}