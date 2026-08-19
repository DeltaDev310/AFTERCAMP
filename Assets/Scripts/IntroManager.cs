using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;

    public CanvasGroup Panel1Group;
    public CanvasGroup Panel2Group;

    public TMP_Text DialogueText;

    private int CurrentLine = 0;

    private string[] Lines = new string[]
    {
       "I've been dreaming about this place...",
       "every night...",
       "that same place...",
        "I need to go back there."
    };

    void Start()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);

        Panel1Group.alpha = 0f;
        Panel2Group.alpha = 0f;

        DialogueText.text = Lines[CurrentLine];

        StartCoroutine(FadeIn(Panel1Group));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }
    private void NextLine()
    {
        CurrentLine++;
        if (CurrentLine >= Lines.Length)
        {
            EndIntro();
            return;
        }
        if (CurrentLine == 2)
        {
            StartCoroutine(switchPanels());
        }
        DialogueText.text = Lines[CurrentLine];
    }
    private IEnumerator switchPanels()
    {
        yield return StartCoroutine(FadeOut(Panel1Group));
        Panel1.SetActive(false);
        Panel2.SetActive(true);
        yield return StartCoroutine(FadeIn(Panel2Group));
    }

    private IEnumerator FadeIn(CanvasGroup group)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
        }
        group.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup group)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
        }
        group.alpha = 0f;
    }
    void EndIntro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

}
