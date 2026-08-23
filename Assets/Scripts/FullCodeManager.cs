using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullCodeManager : MonoBehaviour
{
    public List<int> fullcode = new List<int>();

    public TMP_Text CodeText;

    public void AddDigit(int digit)
    {
        fullcode.Add(digit);
        CodeText.text = "Code: " + string.Join(", ", fullcode);
        Debug.Log(string.Join(", ", fullcode));
    }
}
