using TMPro;
using UnityEngine;

public class PhotoCounter : MonoBehaviour
{
    public TMP_Text NumberOfPhotosCollected;

    public int NumberOfPhotosCollectedInt = 0;

    private void Start()
    {
        NumberOfPhotosCollected.gameObject.SetActive(false);
    }

    public void AddPhoto()
    {
        if (NumberOfPhotosCollectedInt < 5)
        {
            NumberOfPhotosCollectedInt++;
        }

        NumberOfPhotosCollected.gameObject.SetActive(true);

        if (NumberOfPhotosCollectedInt < 5)
        {
            NumberOfPhotosCollected.text =
                NumberOfPhotosCollectedInt + " / 5\nCollect the photos";
        }
        else
        {
            NumberOfPhotosCollected.text =
                "5 / 5\nPut the code in the entrance door and escape.";
        }
    }
}