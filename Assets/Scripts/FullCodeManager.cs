using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullCodeManager : MonoBehaviour
{
    public static FullCodeManager Instance;

    public List<int> fullcode = new List<int>();
    public TMP_Text CodeText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDigit(int digit)
    {
        fullcode.Add(digit);

        if (CodeText != null)
        {
            CodeText.text = "Code: " + string.Join(", ", fullcode);
        }

        Debug.Log(string.Join(", ", fullcode));
    }
}