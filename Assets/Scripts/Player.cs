using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    Door currentDoor;

    public int collectables;
    public int points;
    public float currentHP = 100f;

    [SerializeField]
    float maxHP = 100f;

    [SerializeField] TextMeshProUGUI collectablesText;

    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] TextMeshProUGUI hpText;

    [SerializeField] TextMeshProUGUI countdownText;

    [Header("Respawn")]
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject gameOverScreen;

    bool inPoisonGas = false;
    bool inLava = false;
    bool isDead = false;

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

        SetCollectablesText();
        SetPointsText();
        SetHPText();
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
        if (inPoisonGas)
            TakeDamage(1f * Time.deltaTime);

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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (currentDoor != null && !currentDoor.IsOpen())
                currentDoor = null;
        }
    }
}