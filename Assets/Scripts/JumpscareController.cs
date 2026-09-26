using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareController : MonoBehaviour
{
    [SerializeField] private GameObject jumpscareBackground;
    [SerializeField] private GameObject jumpscareImage;
    [SerializeField] private AudioSource jumpscareAudio;

    public GameObject Canvas;
    public GameObject Player;
    public GameObject Unraveler;

    public JumpscareController jumpscareController;

    private bool playing = false;

    private void Start()
    {
        jumpscareBackground.SetActive(false);
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

        jumpscareBackground.SetActive(true);
        jumpscareImage.SetActive(true);

        jumpscareAudio.Play();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Menu");

        Canvas.SetActive(false);
        Player.SetActive(false);
        Unraveler.SetActive(false);
    }
}