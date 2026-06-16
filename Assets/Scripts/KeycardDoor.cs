using UnityEngine;
using TMPro;

public class KeycardDoor : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] GameObject promptPanel;
    [SerializeField] TextMeshProUGUI promptText;

    [Header("Audio")]
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip lockedSound;

    [Header("Settings")]
    Animator myAnimator;
    AudioSource audioSource;
    bool isOpen = false;

    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (promptPanel != null)
            promptPanel.SetActive(false);

        if (promptText != null)
        {
            promptText.text = "Get Keycard to open the door";
            Debug.Log("Prompt text set: " + promptText.text);
        }

        else
        {
            Debug.Log("Prompt text is null - not assigned in Inspector");
        }
    }

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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptPanel != null)
                promptPanel.SetActive(false);
        }
    }

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

    public bool IsOpen()
    {
        return isOpen;
    }
}