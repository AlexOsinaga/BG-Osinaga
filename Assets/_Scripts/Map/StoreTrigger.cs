//**Osinaga Yujra Gaabriel Alex**
/// <summary>
////When the assasin is near the door if he presses E he shall enter the store
/// </summary>

using UnityEngine;
using UnityEngine.InputSystem;

public class StoreTrigger : MonoBehaviour
{
    public GameObject pressEBubble; // Reference to the "Press E" bubble GameObject
    public GameObject arrowGuide;
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
            arrowGuide.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpenUI = false;
            pressEBubble.SetActive(false);
            arrowGuide.SetActive(true);
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
                //if (uiMenu == null) // Check if menu doesn't exist(already halndled but i'll keep it to test)
                //{
                    uiMenu = Instantiate(uiMenuPrefab, transform.position, Quaternion.identity);
                    uiMenu.SetActive(true);
                    // Activate close camera
                    closeCamera.SetActive(true);
                    FindObjectOfType<AssassinMovement>().isOnMenu = true; 
                //}
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