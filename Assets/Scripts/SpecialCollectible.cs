// =============================================================================
// SpecialCollectible.cs
// Handles special collectible items like the keycard and gasmask that the
// player picks up by pressing E, granting them special abilities or access.
// =============================================================================

using UnityEngine;
using TMPro;

public class SpecialCollectible : MonoBehaviour
{
    /// <summary>The type of special collectible this object represents.</summary>
    public enum ItemType { Keycard, Gasmask }

    [Header("Item Settings")]
    /// <summary>The item type set in the Inspector to determine what this collectible does.</summary>
    public ItemType itemType;

    [Header("Rotation Settings")]
    /// <summary>The axis and direction the collectible rotates.</summary>
    [SerializeField] private Vector3 rotation;

    /// <summary>The speed at which the collectible rotates.</summary>
    [SerializeField] private float speed;

    [Header("UI")]
    /// <summary>The prompt text shown when the player is near the collectible.</summary>
    [SerializeField] TextMeshProUGUI promptText;

    [Header("Audio")]
    /// <summary>The sound played when the collectible is picked up.</summary>
    [SerializeField] AudioClip collectSound;

    [Header("Effects")]
    /// <summary>The particle effect spawned when the collectible is picked up.</summary>
    [SerializeField] GameObject collectEffect;

    /// <summary>Hides the prompt text when the game starts.</summary>
    void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    /// <summary>Rotates the collectible every frame.</summary>
    void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }

    /// <summary>Shows the prompt text when the player enters the trigger zone.</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = "Press E to pick up " + itemType.ToString();
            }
        }
    }

    /// <summary>Hides the prompt text when the player exits the trigger zone.</summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }

    /// <summary>Collects the item, updates the player's inventory and destroys this object.</summary>
    /// <param name="player">The player that collected this item.</param>
    public void Collect(Player player)
    {
        switch (itemType)
        {
            case ItemType.Keycard:
                player.hasKeycard = true;
                break;
            case ItemType.Gasmask:
                player.hasGasmask = true;
                break;
        }

        player.UpdateInventoryText();

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, transform.rotation);
        }

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
        
        Destroy(gameObject);
    }
}