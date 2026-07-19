using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    private bool isOpen = false;

    private bool playerNearby = false;

    public SpriteRenderer doorSprite;
    public BoxCollider2D doorCollider;

    public void OpenDoor()
    {
        doorSprite.enabled = false;
        doorCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }


}
