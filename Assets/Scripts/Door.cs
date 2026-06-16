using UnityEngine;

public class Door : MonoBehaviour
{
    Animator myAnimator;
    bool isOpen = false;

    [Header("Audio")]
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    AudioSource audioSource;

    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (myAnimator != null)
        {
            if (!isOpen)
            {
                myAnimator.ResetTrigger("DoorClose");
                myAnimator.SetTrigger("DoorOpen");
                Debug.Log("Playing open sound: " + (openSound != null));
                if (audioSource != null && openSound != null)
                    audioSource.PlayOneShot(openSound);
            }
            else
            {
                myAnimator.ResetTrigger("DoorOpen");
                myAnimator.SetTrigger("DoorClose");
                Debug.Log("Playing close sound: " + (closeSound != null));
                if (audioSource != null && closeSound != null)
                    audioSource.PlayOneShot(closeSound);
            }

            isOpen = !isOpen;
        }
    }

    public void ForceClose()
    {
        if (isOpen && myAnimator != null)
        {
            myAnimator.ResetTrigger("DoorOpen");
            myAnimator.SetTrigger("DoorClose");
            if (audioSource != null && closeSound != null)
                audioSource.PlayOneShot(closeSound);
            isOpen = false;
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}