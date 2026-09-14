using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public FullCodeManager fullCodeManager;
    public PhotoCounter photoCounter;

    public int PhotoNumber;

    private void Start()
    {
        // If this photo was already collected, remove it immediately.
        if (PhotoProgress.Instance.HasCollected(PhotoNumber))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectPhoto();
        }
    }

    private void CollectPhoto()
    {
        // Remember that THIS specific photo was collected.
        PhotoProgress.Instance.MarkCollected(PhotoNumber);

        // Generate and store the random digit.
        int photoCodeDigit = Random.Range(0, 10);
        fullCodeManager.AddDigit(photoCodeDigit);

        // Update the counter.
        photoCounter.AddPhoto();

        // Remove the photo.
        Destroy(gameObject);
    }
}