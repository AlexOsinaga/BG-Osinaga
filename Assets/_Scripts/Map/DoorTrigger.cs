//**Osinaga Yujra Gaabriel Alex**
/// <summary>
////player gets close the aguiding arrow will dissapear
////i wanted to use this as a base for interactions so store trigger could inherrit from this but i noticed the'll be diffrent 
/// </summary>
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad = "ClothingStore";
    public GameObject interactionPrompt; // Reference to the UI interaction prompt
    public GameObject arrowGuide;
    private bool playerInRange = false;

    private void Start()
    {
        interactionPrompt.SetActive(false); // Ensure the interaction prompt is initially disabled
        arrowGuide.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionPrompt.SetActive(true); // Display the interaction prompt
            arrowGuide.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionPrompt.SetActive(false); // Hide the interaction prompt
            arrowGuide.SetActive(true);
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
