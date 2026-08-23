using TMPro;
using UnityEngine;

public class PhotoCounter : MonoBehaviour
{
    public TMP_Text NumberOfPhotosCollected;
    public int NumberOfPhotosCollectedInt = 0;

    private void Start()
    {
        NumberOfPhotosCollected.text = NumberOfPhotosCollectedInt + " / 5";
        NumberOfPhotosCollected.gameObject.SetActive(false);
    }
    public void AddPhoto()
    {

        if (NumberOfPhotosCollectedInt < 1)
        {
            NumberOfPhotosCollected.gameObject.SetActive(true);
        }
        NumberOfPhotosCollectedInt++;
        NumberOfPhotosCollected.text = NumberOfPhotosCollectedInt + " / 5";
    }
}
