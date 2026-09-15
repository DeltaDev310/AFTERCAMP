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
    public GameObject ErrorMessageText;
    public GameObject EnterCodeMessage;

    void Start()
    {


        PasscodeInputField.gameObject.SetActive(false);

        PasscodeInputField.onSubmit.AddListener(CheckPassCode);

    }
    void Update()
    {
        Passcode = string.Join("", FullCodeManager.fullcode);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PasscodeInputField.gameObject.SetActive(true);

            PasscodeInputField.Select();
            PasscodeInputField.ActivateInputField();

            EnterCodeMessage.SetActive(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterCodeMessage.SetActive(false);
        }
    }
    void CheckPassCode(string input)
    {
        if (input == Passcode && Passcode != null && Passcode.Length == 5)
        {
            Debug.Log("Correct Passcode!");
            SceneManager.LoadScene("End");

            Player.gameObject.SetActive(false);
            EntraceDoor.gameObject.SetActive(false);
            PasscodeInputField.gameObject.SetActive(false);
            Canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Incorrect Passcode! try again");

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


