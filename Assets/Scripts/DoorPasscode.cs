using TMPro;
using UnityEngine;

public class DoorPasscode : MonoBehaviour
{
    [SerializeField] private string Passcode;
    public FullCodeManager FullCodeManager;

    public TMP_InputField PasscodeInputField;

    void Start()
    {
        Passcode = FullCodeManager.fullcode.ToString();

        PasscodeInputField.gameObject.SetActive(false);

        PasscodeInputField.onSubmit.AddListener(CheckPassCode);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PasscodeInputField.gameObject.SetActive(true);

            PasscodeInputField.Select();
            PasscodeInputField.ActivateInputField();
        }
    }
    void CheckPassCode(string input)
    {
        if (input == Passcode)
        {
            Debug.Log("Correct Passcode!");
        }
        else
        {
            Debug.Log("Incorrect Passcode! try again");

            PasscodeInputField.gameObject.SetActive(false);
        }
    }
}
