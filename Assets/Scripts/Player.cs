using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Door currentDoor; // Store the door object the player is currently able to interact with

    public int collectables;
    public int points;
    public float currentHP = 100f;

    [SerializeField]
    int targetPoints = 50;

    [SerializeField]
    TextMeshProUGUI collectablesText;

    [SerializeField]
    TextMeshProUGUI pointsText;

    void Start()
    {
        collectables = 0;
        points = 0;

        SetCollectablesText();
        SetPointsText();
    }

    void Update()
    {
        HandleDoorAutoClose();
    }

    void HandleDoorAutoClose()
    {
        if (currentDoor != null)
        {
            float distance = Vector3.Distance(transform.position, currentDoor.transform.position);
            if (distance > 5f)
            {
                currentDoor.ForceClose();
            }
        }
    }

    void OnInteract() // Called automatically by Player Input via Send Messages when E is pressed
    {
        if (currentDoor != null) // Only interact with a door if the player is currently near one
        {
            Door doorScript = currentDoor.GetComponentInParent<Door>(); // Find the door script on the door object or its parents
            if (doorScript != null) // Check if the door script was found successfully
            {
                currentDoor.Interact();
            }
            else
            {
                Debug.Log("No door nearby");
            }
        }
    }

    void SetCollectablesText()
    {
        collectablesText.text = "Collectables: " + collectables.ToString() + " / 40";
    }

    void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectibles"))
        {
            Collectibles col = other.GetComponentInParent<Collectibles>();
            if (col != null)
            {
                collectables++;
                points += col.points;

                col.Collect();

                SetCollectablesText();
                SetPointsText();
            }
        }

        if (other.gameObject.CompareTag("Door")) // Check if the object entering the trigger is tagged as a door
        {
            currentDoor = other.GetComponentInParent<Door>(); // Store the door script so the player can interact with it later
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
        // only clear the door reference if it is already closed
        if (currentDoor != null && !currentDoor.IsOpen())
            currentDoor = null;
        }
    }
}   