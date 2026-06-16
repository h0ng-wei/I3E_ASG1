// =============================================================================
// EndZone.cs
// Detects when the player enters the end zone and either shows the end screen
// if they have enough points or displays a requirement message if they do not.
// =============================================================================

using UnityEngine;
using TMPro;

public class EndZone : MonoBehaviour
{
    [Header("UI References")]
    /// <summary>The end screen panel shown when the player wins.</summary>
    [SerializeField] GameObject endScreen;

    /// <summary>The panel shown behind the requirement text.</summary>
    [SerializeField] GameObject requirementPanel;

    /// <summary>The text shown when the player does not have enough points.</summary>
    [SerializeField] TextMeshProUGUI requirementText;

    /// <summary>The text shown on the end screen displaying the player's final score.</summary>
    [SerializeField] TextMeshProUGUI endScoreText;

    /// <summary>The text shown on the end screen displaying the time taken.</summary>
    [SerializeField] TextMeshProUGUI endTimeText;

    [Header("Audio")]
    /// <summary>The sound played when the player wins.</summary>
    [SerializeField] private AudioClip winSound;

    [Header("Settings")]
    /// <summary>The number of points required to win the game.</summary>
    [SerializeField] int targetPoints = 50;

    /// <summary>Hides the end screen and requirement panel on scene load.</summary>
    void Start()
    {
        if (endScreen != null)
            endScreen.SetActive(false);

        if (requirementPanel != null)
            requirementPanel.SetActive(false);
    }

    /// <summary>Checks if the player has enough points when entering the end zone.</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (player.points >= targetPoints)
            {
                ShowEndScreen(player);
            }
            else
            {
                if (requirementPanel != null)
                    requirementPanel.SetActive(true);

                if (requirementText != null)
                    requirementText.text = "Achieve " + targetPoints + " points to win";
            }
        }
    }

    /// <summary>Hides the requirement panel when the player exits the end zone.</summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requirementPanel != null)
                requirementPanel.SetActive(false);
        }
    }

    /// <summary>Shows the end screen with the player's final score and time taken.</summary>
    /// <param name="player">The player that reached the end zone.</param>
    private void ShowEndScreen(Player player)
    {
        GameTimer.StopTimer();

        if (winSound != null)
            AudioSource.PlayClipAtPoint(winSound, transform.position);

        if (endScreen != null)
        {
            endScreen.SetActive(true);

            if (endScoreText != null)
                endScoreText.text = player.points.ToString();

            if (endTimeText != null)
                endTimeText.text = GameTimer.GetFormattedTime();
        }

        Time.timeScale = 0f;
    }
}