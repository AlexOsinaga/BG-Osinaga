using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        AudioSystem.Instance.PlaySound(clickSound);
    }
}