using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;

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

        DialogueText.text = Lines[CurrentLine];
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
            Panel1.SetActive(false);
            Panel2.SetActive(true);
        }
        DialogueText.text = Lines[CurrentLine];
    }
    void EndIntro()
    {
        Debug.Log("Intro finished. Proceeding to the next scene...");
    }

}
