// =============================================================================
// Door.cs
// Controls the door's open and close animations using an Animator component,
// playing audio feedback and allowing the door to be force closed from a distance.
// =============================================================================

using UnityEngine;

public class Door : MonoBehaviour
{
    /// <summary>The Animator component that controls the door's animations.</summary>
    Animator myAnimator;

    /// <summary>Whether the door is currently open.</summary>
    bool isOpen = false;

    [Header("Audio")]
    /// <summary>The sound played when the door opens.</summary>
    [SerializeField] AudioClip openSound;

    /// <summary>The sound played when the door closes.</summary>
    [SerializeField] AudioClip closeSound;

    /// <summary>The AudioSource component used to play door sounds.</summary>
    AudioSource audioSource;

    /// <summary>Gets the Animator and AudioSource components on scene load.</summary>
    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Toggles the door open or closed and plays the appropriate sound.</summary>
    public void Interact()
    {
        if (myAnimator != null)
        {
            if (!isOpen)
            {
                myAnimator.ResetTrigger("DoorClose");
                myAnimator.SetTrigger("DoorOpen");
                if (audioSource != null && openSound != null)
                    audioSource.PlayOneShot(openSound);
            }
            else
            {
                myAnimator.ResetTrigger("DoorOpen");
                myAnimator.SetTrigger("DoorClose");
                if (audioSource != null && closeSound != null)
                    audioSource.PlayOneShot(closeSound);
            }

            isOpen = !isOpen;
        }
    }

    /// <summary>Forces the door to close if it is currently open.</summary>
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

    /// <summary>Returns whether the door is currently open.</summary>
    public bool IsOpen()
    {
        return isOpen;
    }
}