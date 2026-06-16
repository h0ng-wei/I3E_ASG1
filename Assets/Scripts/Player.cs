using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    Door currentDoor;
    SpecialCollectible currentSpecialCollectible;
    KeycardDoor currentKeycardDoor;

    public int collectables;
    public int points;
    public float currentHP = 50f;

    [SerializeField] float maxHP = 50f;

    [SerializeField] TextMeshProUGUI collectablesText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] GameObject inventoryPanel;

    [Header("Respawn")]
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] AudioClip gameOverSound;

    bool inPoisonGas = false;
    bool inLava = false;
    bool isDead = false;
    public bool hasKeycard = false;
    public bool hasGasmask = false;

    public void EnterPoisonGas() => inPoisonGas = true;
    public void ExitPoisonGas() => inPoisonGas = false;
    public void EnterLava() => inLava = true;
    public void ExitLava() => inLava = false;

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

    void Update()
    {
        HandleDoorAutoClose();
        HandleDamageOverTime();
    }

    void HandleDoorAutoClose()
    {
        if (currentDoor != null)
        {
            float distance = Vector3.Distance(transform.position, currentDoor.transform.position);
            if (distance > 5f)
                currentDoor.ForceClose();
        }
    }

    void HandleDamageOverTime()
    {
        if (inPoisonGas && !hasGasmask)
            TakeDamage(2f * Time.deltaTime);

        if (inLava)
            TakeDamage(10f * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        SetHPText();

        if (currentHP <= 0)
            Die();
    }

    public void InstantDeath()
    {
        if (isDead) return;
        currentHP = 0;
        SetHPText();
        Die();
    }

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

    void OnInteract()
    {
        if (currentDoor != null)
        {
            Door doorScript = currentDoor.GetComponentInParent<Door>();
            if (doorScript != null)
                currentDoor.Interact();
            else
                print("Error: No Door found on ");
        }

        if (currentKeycardDoor != null)
            currentKeycardDoor.Interact(this);

        if (currentSpecialCollectible != null)
            currentSpecialCollectible.Collect(this);
    }

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

    void SetCollectablesText()
    {
        collectablesText.text = "Collectables: " + collectables.ToString() + " / 40";
    }

    void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    void SetHPText()
    {
        if (hpText != null)
            hpText.text = "HP: " + Mathf.CeilToInt(currentHP).ToString();
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

        if (other.gameObject.CompareTag("Door"))
            currentDoor = other.GetComponentInParent<Door>();

        if (other.gameObject.CompareTag("KeycardDoor"))
            currentKeycardDoor = other.GetComponentInParent<KeycardDoor>();

        if (other.gameObject.CompareTag("SpecialCollectible"))
            currentSpecialCollectible = other.GetComponentInParent<SpecialCollectible>();
    }

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