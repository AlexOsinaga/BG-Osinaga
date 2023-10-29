using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    private bool isMuted = false;

    private void Start()
    {
        // Initialize the toggle based on the current mute state (if saved).
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        Toggle toggle = GetComponent<Toggle>();
        toggle.isOn = isMuted;

        // Apply the mute state.
        AudioListener.volume = isMuted ? 0 : 1;

        // Add a listener to the toggle's onValueChanged event.
        toggle.onValueChanged.AddListener(ToggleSoundMute);
    }

    private void ToggleSoundMute(bool isMuted)
    {
        this.isMuted = isMuted;
        AudioListener.volume = isMuted ? 0 : 1;

        // Save the mute state (0 for unmuted, 1 for muted).
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
