using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    [Header("Player")]
    public Transform player;
    public GameObject playerMovement;

    [Header("Fade")]
    public Image fadeImage;

    [Header("Credits")]
    public TMP_Text madeByText;

    [Header("Movement")]
    public float walkSpeed = 2f;

    [Header("Timing")]
    public float dialogueDelay = 1.5f;
    public float dialogueDuration = 5f;
    public float walkAfterDialogue = 7f;
    public float fadeDuration = 3f;
    public float blackScreenDelay = 1f;
    public float creditDuration = 5f;

    private void Start()
    {
        // Disable player controls during the ending
        if (playerMovement != null)
            playerMovement.SetActive(false);

        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        // =========================
        // INITIAL SETUP
        // =========================

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (dialogueText != null)
            dialogueText.gameObject.SetActive(false);

        if (madeByText != null)
            madeByText.gameObject.SetActive(false);

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }

        // Make sure PlayerMovement is disabled
        if (player != null)
        {
            PlayerMovement movement =
                player.GetComponent<PlayerMovement>();

            if (movement != null)
                movement.enabled = false;
        }

        // =========================
        // PLAYER STARTS WALKING
        // =========================

        float timer = 0f;

        while (timer < dialogueDelay)
        {
            WalkPlayer();

            timer += Time.deltaTime;

            yield return null;
        }

        // =========================
        // SHOW DIALOGUE
        // =========================

        if (dialogueText != null)
        {
            dialogueText.text = "\". . .\"";
            dialogueText.gameObject.SetActive(true);
        }

        if (dialogueBox != null)
            dialogueBox.SetActive(true);

        // Keep walking while dialogue is visible
        timer = 0f;

        while (timer < dialogueDuration)
        {
            WalkPlayer();

            timer += Time.deltaTime;

            yield return null;
        }

        // =========================
        // HIDE DIALOGUE
        // =========================

        if (dialogueText != null)
            dialogueText.gameObject.SetActive(false);

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        // =========================
        // WALK A LITTLE LONGER
        // =========================

        timer = 0f;

        while (timer < walkAfterDialogue)
        {
            WalkPlayer();

            timer += Time.deltaTime;

            yield return null;
        }

        // =========================
        // FADE TO BLACK
        // =========================

        if (fadeImage != null)
        {
            timer = 0f;

            Color color = fadeImage.color;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                color.a =
                    Mathf.Lerp(
                        0f,
                        1f,
                        timer / fadeDuration
                    );

                fadeImage.color = color;

                yield return null;
            }

            color.a = 1f;
            fadeImage.color = color;
        }

        // =========================
        // BLACK SCREEN
        // =========================

        yield return new WaitForSeconds(
            blackScreenDelay
        );

        // =========================
        // AFTERCAMP BY DELTA
        // =========================

        if (madeByText != null)
        {
            madeByText.text = "AFTERCAMP BY DELTA";
            madeByText.gameObject.SetActive(true);
        }

        // =========================
        // WAIT FOR CREDITS
        // =========================

        yield return new WaitForSeconds(
            creditDuration
        );

        // =========================
        // ENABLE PLAYER MOVEMENT
        // =========================

        if (playerMovement != null)
        {
            playerMovement.SetActive(true);
        }

        // Also make sure the PlayerMovement component itself is enabled
        if (player != null)
        {
            PlayerMovement movement =
                player.GetComponent<PlayerMovement>();

            if (movement != null)
                movement.enabled = true;
        }

        // =========================
        // MAIN MENU
        // =========================

        QuitGame();
    }

    private void WalkPlayer()
    {
        if (player == null)
            return;

        player.position +=
            Vector3.down *
            walkSpeed *
            Time.deltaTime;
    }
    private void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}