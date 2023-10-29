using UnityEngine;
using UnityEngine.InputSystem;

public class StoreTrigger : MonoBehaviour
{
    public GameObject pressEBubble; // Reference to the "Press E" bubble GameObject
    public GameObject uiMenuPrefab; // The UI menu prefab
    public GameObject closeCamera; // Added reference

    private GameObject uiMenu;
    private bool canOpenUI = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpenUI = true;
            pressEBubble.SetActive(true); // Enable the "Press E" bubble from the scene
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpenUI = false;
            pressEBubble.SetActive(false);
            if (uiMenu != null)
            {
                uiMenu.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (canOpenUI)
        {
            // Check for "E" key press using the new Input System
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                //if (uiMenuPrefab != null)
                if (uiMenu == null) // Check if menu doesn't exist
                {
                    uiMenu = Instantiate(uiMenuPrefab, transform.position, Quaternion.identity);
                    uiMenu.SetActive(true);
                    // Activate close camera
                    closeCamera.SetActive(true);
                    FindObjectOfType<AssassinMovement>().isOnMenu = true; 
                }
            }
            if (Keyboard.current.escapeKey.wasPressedThisFrame && uiMenu != null)
            {
                CloseMenu(); 
            }
        }
    }
    public void CloseMenu()
    {
        uiMenu.SetActive(false);
        closeCamera.SetActive(false);
        
        // Enable player movement
        FindObjectOfType<AssassinMovement>().isOnMenu = false;
        
        Destroy(uiMenu);
    }
}