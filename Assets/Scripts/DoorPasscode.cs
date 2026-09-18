using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPasscode : MonoBehaviour
{
    public string Passcode;
    public FullCodeManager FullCodeManager;

    public TMP_InputField PasscodeInputField;

    public Transform Player;
    public Transform EntraceDoor;
    public Transform Canvas;
    public Transform TheUnraveler;

    public GameObject ErrorMessageText;
    public GameObject EnterCodeMessage;

    public bool PlayerAtDoor;

    void Start()
    {
        PasscodeInputField.gameObject.SetActive(false);

        ErrorMessageText.SetActive(false);
        EnterCodeMessage.SetActive(false);

        PasscodeInputField.onSubmit.AddListener(CheckPassCode);
    }

    void Update()
    {
        Passcode = string.Join("", FullCodeManager.fullcode);

        if (PlayerAtDoor && Input.GetKeyDown(KeyCode.E))
        {
            PasscodeInputField.gameObject.SetActive(true);

            PasscodeInputField.Select();
            PasscodeInputField.ActivateInputField();

            EnterCodeMessage.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAtDoor = true;
            EnterCodeMessage.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAtDoor = false;
            EnterCodeMessage.SetActive(false);
        }
    }

    void CheckPassCode(string input)
    {
        if (input == Passcode && Passcode != null && Passcode.Length == 5)
        {
            Debug.Log("Correct Passcode!");

            Player.gameObject.SetActive(false);
            EntraceDoor.gameObject.SetActive(false);
            PasscodeInputField.gameObject.SetActive(false);
            Canvas.gameObject.SetActive(false);
            TheUnraveler.gameObject.SetActive(false);

            SceneManager.LoadScene("End");
        }
        else
        {
            Debug.Log("Incorrect Passcode! Try again");

            PasscodeInputField.gameObject.SetActive(false);

            StartCoroutine(ErrorMessage());
        }
    }

    private IEnumerator ErrorMessage()
    {
        ErrorMessageText.SetActive(true);

        yield return new WaitForSeconds(2f);

        ErrorMessageText.SetActive(false);
    }
}