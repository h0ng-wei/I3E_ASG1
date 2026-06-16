// =============================================================================
// Player.cs
// Manages the player's stats, interactions, damage, death and respawning.
// =============================================================================

using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    /// <summary>The door the player is currently near and able to interact with.</summary>
    Door currentDoor;

    /// <summary>The special collectible the player is currently near and able to pick up.</summary>
    SpecialCollectible currentSpecialCollectible;

    /// <summary>The keycard door the player is currently near and able to interact with.</summary>
    KeycardDoor currentKeycardDoor;

    /// <summary>The number of collectibles the player has collected.</summary>
    public int collectables;

    /// <summary>The total points the player has earned.</summary>
    public int points;

    /// <summary>The player's current HP.</summary>
    public float currentHP = 50f;

    /// <summary>The player's maximum HP.</summary>
    [SerializeField] float maxHP = 50f;

    /// <summary>UI text that displays the number of collectables collected.</summary>
    [SerializeField] TextMeshProUGUI collectablesText;

    /// <summary>UI text that displays the player's current points.</summary>
    [SerializeField] TextMeshProUGUI pointsText;

    /// <summary>UI text that displays the player's current HP.</summary>
    [SerializeField] TextMeshProUGUI hpText;

    /// <summary>UI text that displays the respawn countdown.</summary>
    [SerializeField] TextMeshProUGUI countdownText;

    /// <summary>UI text that displays the player's inventory items.</summary>
    [SerializeField] TextMeshProUGUI inventoryText;

    /// <summary>The panel behind the inventory text.</summary>
    [SerializeField] GameObject inventoryPanel;

    [Header("Respawn")]
    /// <summary>The transform the player respawns at when they die.</summary>
    [SerializeField] Transform respawnPoint;

    /// <summary>The game over screen shown when the player dies.</summary>
    [SerializeField] GameObject gameOverScreen;

    /// <summary>The sound played when the player dies.</summary>
    [SerializeField] AudioClip gameOverSound;

    /// <summary>Whether the player is currently in poison gas.</summary>
    bool inPoisonGas = false;

    /// <summary>Whether the player is currently in lava.</summary>
    bool inLava = false;

    /// <summary>Whether the player is currently dead.</summary>
    bool isDead = false;

    /// <summary>Whether the player has collected the keycard.</summary>
    public bool hasKeycard = false;

    /// <summary>Whether the player has collected the gasmask.</summary>
    public bool hasGasmask = false;

    /// <summary>Sets the player as being in poison gas.</summary>
    public void EnterPoisonGas() => inPoisonGas = true;

    /// <summary>Sets the player as no longer being in poison gas.</summary>
    public void ExitPoisonGas() => inPoisonGas = false;

    /// <summary>Sets the player as being in lava.</summary>
    public void EnterLava() => inLava = true;

    /// <summary>Sets the player as no longer being in lava.</summary>
    public void ExitLava() => inLava = false;

    /// <summary>Initializes the player's stats and UI on scene load.</summary>
    void Start()
    {
        collectables = 0;
        points = 0;
        currentHP = maxHP;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (inventoryPanel != null)
            inventoryPanel.SetActive(true);

        SetCollectablesText();
        SetPointsText();
        SetHPText();
        UpdateInventoryText();
    }

    /// <summary>Handles door auto close and damage over time every frame.</summary>
    void Update()
    {
        HandleDoorAutoClose();
        HandleDamageOverTime();
    }

    /// <summary>Closes the current door automatically when the player walks too far away.</summary>
    void HandleDoorAutoClose()
    {
        if (currentDoor != null)
        {
            float distance = Vector3.Distance(transform.position, currentDoor.transform.position);
            if (distance > 5f)
                currentDoor.ForceClose();
        }
    }

    /// <summary>Applies damage over time when the player is in a danger zone.</summary>
    void HandleDamageOverTime()
    {
        if (inPoisonGas && !hasGasmask)
            TakeDamage(2f * Time.deltaTime);

        if (inLava)
            TakeDamage(10f * Time.deltaTime);
    }

    /// <summary>Reduces the player's HP by the given amount and triggers death if HP reaches zero.</summary>
    /// <param name="amount">The amount of damage to deal.</param>
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        SetHPText();

        if (currentHP <= 0)
            Die();
    }

    /// <summary>Instantly kills the player.</summary>
    public void InstantDeath()
    {
        if (isDead) return;
        currentHP = 0;
        SetHPText();
        Die();
    }

    /// <summary>Handles the player's death by showing the game over screen and starting the respawn countdown.</summary>
    void Die()
    {
        if (isDead) return;
        isDead = true;

        currentHP = 0;
        inPoisonGas = false;
        inLava = false;
        SetHPText();

        if (gameOverSound != null)
            AudioSource.PlayClipAtPoint(gameOverSound, transform.position);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        StartCoroutine(Countdown());
    }

    /// <summary>Counts down from 3 and then respawns the player.</summary>
    IEnumerator Countdown()
    {
        int count = 3;
        while (count > 0)
        {
            if (countdownText != null)
                countdownText.text = "Respawning in " + count;
            yield return new WaitForSeconds(1f);
            count--;
        }

        if (countdownText != null)
            countdownText.text = "";

        Respawn();
    }

    /// <summary>Respawns the player at the respawn point and resets their stats.</summary>
    public void Respawn()
    {
        isDead = false;
        currentHP = maxHP;
        inPoisonGas = false;
        inLava = false;
        SetHPText();

        CharacterController cc = GetComponentInParent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            transform.position = respawnPoint.position;
            cc.enabled = true;
        }
        else
        {
            transform.position = respawnPoint.position;
        }

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    /// <summary>Called automatically by the Input System when the player presses E to interact.</summary>
    void OnInteract()
    {
        if (currentDoor != null)
        {
            Door doorScript = currentDoor.GetComponentInParent<Door>();
            if (doorScript != null)
                currentDoor.Interact();
            else
                print("Error: No Door found on " + currentDoor.name);
        }

        if (currentKeycardDoor != null)
            currentKeycardDoor.Interact(this);

        if (currentSpecialCollectible != null)
            currentSpecialCollectible.Collect(this);
    }

    /// <summary>Updates the inventory UI text with the player's current items.</summary>
    public void UpdateInventoryText()
    {
        if (inventoryText == null) return;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(true);

        string inventory = "Inventory\n\n";

        if (hasKeycard)
            inventory += "Keycard\n";

        if (hasGasmask)
            inventory += "Gasmask\n";

        inventoryText.text = inventory;
    }

    /// <summary>Updates the collectables UI text.</summary>
    void SetCollectablesText()
    {
        collectablesText.text = "Collectables: " + collectables.ToString() + " / 40";
    }

    /// <summary>Updates the points UI text.</summary>
    void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    /// <summary>Updates the HP UI text.</summary>
    void SetHPText()
    {
        if (hpText != null)
            hpText.text = "HP: " + Mathf.CeilToInt(currentHP).ToString();
    }

    /// <summary>Detects when the player enters a trigger collider and handles the appropriate interaction.</summary>
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

        if (other.gameObject.CompareTag("Door"))
            currentDoor = other.GetComponentInParent<Door>();

        if (other.gameObject.CompareTag("KeycardDoor"))
            currentKeycardDoor = other.GetComponentInParent<KeycardDoor>();

        if (other.gameObject.CompareTag("SpecialCollectible"))
            currentSpecialCollectible = other.GetComponentInParent<SpecialCollectible>();
    }

    /// <summary>Detects when the player exits a trigger collider and clears the appropriate reference.</summary>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (currentDoor != null && !currentDoor.IsOpen())
                currentDoor = null;
        }

        if (other.gameObject.CompareTag("KeycardDoor"))
        {
            currentKeycardDoor = null;
        }

        if (other.gameObject.CompareTag("SpecialCollectible"))
        {
            currentSpecialCollectible = null;
        }
    }
}