using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareController : MonoBehaviour
{
    public static JumpscareController Instance;

    [Header("Jumpscare")]
    public GameObject jumpscareImage;
    public AudioSource jumpscareAudio;

    private bool playing = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);
    }

    public void PlayJumpscare()
    {
        if (playing)
            return;

        StartCoroutine(JumpscareSequence());
    }

    private IEnumerator JumpscareSequence()
    {
        playing = true;

        Debug.Log("JUMPSCARE STARTED");

        // Show image
        if (jumpscareImage != null)
            jumpscareImage.SetActive(true);

        // Play sound
        if (jumpscareAudio != null)
            jumpscareAudio.Play();

        yield return new WaitForSeconds(2f);

        // Stop sound
        if (jumpscareAudio != null)
            jumpscareAudio.Stop();

        // Hide image
        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);

        ResetGame();

        // Go back to intro
        SceneManager.LoadScene("Intro");
    }

    private void ResetGame()
    {
        // Reset code
        if (FullCodeManager.Instance != null)
        {
            FullCodeManager.Instance.fullcode.Clear();
        }

        // Reset photos
        if (PhotoProgress.Instance != null)
        {
            PhotoProgress.Instance.collectedPhotos.Clear();
        }

        Debug.Log("GAME RESET");
    }
}