using UnityEngine;
using TMPro;

public class SpecialCollectible : MonoBehaviour
{
    public enum ItemType { Keycard, Gasmask }

    [Header("Item Settings")]
    public ItemType itemType;

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI promptText;

    [Header("Audio")]
    [SerializeField] AudioClip collectSound;

    [Header("Effects")]
    [SerializeField] GameObject collectEffect;

    void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }

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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }

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

        // play sound
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        // spawn effect
        if (collectEffect != null)
            Instantiate(collectEffect, transform.position, transform.rotation);

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}