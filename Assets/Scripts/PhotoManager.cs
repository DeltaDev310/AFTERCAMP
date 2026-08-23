using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PhotoManager : MonoBehaviour
{
    public FullCodeManager fullCodeManager;

    public int PhotoNumber = 0;
    int PhotoCodeDigit;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectPhoto();
        }
    }
    void CollectPhoto()
    {
        PhotoCodeDigit = Random.Range(0, 10);
        fullCodeManager.AddDigit(PhotoCodeDigit);
        Destroy(gameObject);
    }
}
