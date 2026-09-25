using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareController : MonoBehaviour
{
    public static JumpscareController Instance;

    [SerializeField] private GameObject jumpscareImage;
    [SerializeField] private AudioSource jumpscareAudio;

    private bool playing = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
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

        jumpscareImage.SetActive(true);
        jumpscareAudio.Play();

        yield return new WaitForSeconds(2f);

        jumpscareAudio.Stop();
        jumpscareImage.SetActive(false);

        ResetGame();

        SceneManager.LoadScene("Intro");
    }

    private void ResetGame()
    {
        if (FullCodeManager.Instance != null)
        {
            FullCodeManager.Instance.fullcode.Clear();
            Destroy(FullCodeManager.Instance.gameObject);
        }

        if (PhotoProgress.Instance != null)
        {
            PhotoProgress.Instance.collectedPhotos.Clear();
            Destroy(PhotoProgress.Instance.gameObject);
        }

        // Destroy persistent player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            Destroy(player);
    }
}