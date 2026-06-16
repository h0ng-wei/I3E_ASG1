// =============================================================================
// KeycardDoor.cs
// Controls a locked door that only opens when the player has the keycard,
// showing a prompt panel and playing audio feedback on interaction.
// =============================================================================

using UnityEngine;
using TMPro;

public class KeycardDoor : MonoBehaviour
{
    [Header("UI References")]
    /// <summary>The panel shown behind the prompt text.</summary>
    [SerializeField] GameObject promptPanel;

    /// <summary>The text shown when the player is near the door.</summary>
    [SerializeField] TextMeshProUGUI promptText;

    [Header("Audio")]
    /// <summary>The sound played when the door opens.</summary>
    [SerializeField] AudioClip openSound;

    /// <summary>The sound played when the player tries to open the door without the keycard.</summary>
    [SerializeField] AudioClip lockedSound;

    [Header("Settings")]
    /// <summary>The Animator component that controls the door's open and close animations.</summary>
    Animator myAnimator;

    /// <summary>The AudioSource component used to play door sounds.</summary>
    AudioSource audioSource;

    /// <summary>Whether the door is currently open.</summary>
    bool isOpen = false;

    /// <summary>Initializes the door's components and sets the prompt text on scene load.</summary>
    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (promptPanel != null)
            promptPanel.SetActive(false);

        if (promptText != null)
            promptText.text = "Get Keycard to open the door";
    }

    /// <summary>Shows the prompt panel when the player enters the trigger zone.</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (promptPanel != null)
                promptPanel.SetActive(true);
        }
    }

    /// <summary>Hides the prompt panel when the player exits the trigger zone.</summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptPanel != null)
                promptPanel.SetActive(false);
        }
    }

    /// <summary>Opens the door if the player has the keycard, otherwise plays the locked sound.</summary>
    /// <param name="player">The player attempting to interact with the door.</param>
    public void Interact(Player player)
    {
        if (myAnimator == null) return;

        if (player.hasKeycard)
        {
            if (!isOpen)
            {
                myAnimator.ResetTrigger("DoorClose");
                myAnimator.SetTrigger("DoorOpen");
                isOpen = true;

                if (audioSource != null && openSound != null)
                    audioSource.PlayOneShot(openSound);

                if (promptPanel != null)
                    promptPanel.SetActive(false);
            }
        }
        else
        {
            if (audioSource != null && lockedSound != null)
                audioSource.PlayOneShot(lockedSound);
        }
    }

    /// <summary>Returns whether the door is currently open.</summary>
    public bool IsOpen()
    {
        return isOpen;
    }
}