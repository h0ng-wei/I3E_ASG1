using UnityEngine;

public class Door : MonoBehaviour
{
    Animator myAnimator;
    bool isOpen = false;

    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (myAnimator != null)
        {
            if (!isOpen)
            {
                myAnimator.ResetTrigger("DoorClose");
                myAnimator.SetTrigger("DoorOpen");
            }
            else
            {
                myAnimator.ResetTrigger("DoorOpen");
                myAnimator.SetTrigger("DoorClose");
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
            isOpen = false;
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}