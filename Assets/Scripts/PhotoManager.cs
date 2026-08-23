using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using TMPro;

public class PhotoManager : MonoBehaviour
{
    public FullCodeManager fullCodeManager;

    public int PhotoNumber = 0;
    int PhotoCodeDigit;

    public PhotoCounter photoCounter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectPhoto();
            photoCounter.AddPhoto();
        }
    }
    void CollectPhoto()
    {
        PhotoCodeDigit = Random.Range(0, 10);
        fullCodeManager.AddDigit(PhotoCodeDigit);


        Destroy(gameObject);
    }

    
}
