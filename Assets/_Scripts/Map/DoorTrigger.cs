using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad = "ClothingStore";
    public GameObject interactionPrompt; // Reference to the UI interaction prompt

    private bool playerInRange = false;

    private void Start()
    {
        interactionPrompt.SetActive(false); // Ensure the interaction prompt is initially disabled
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionPrompt.SetActive(true); // Display the interaction prompt
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionPrompt.SetActive(false); // Hide the interaction prompt
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            // Check for "E" key press using the new Input System
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                LoadClothingStoreScene();
            }
        }
    }

    private void LoadClothingStoreScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
