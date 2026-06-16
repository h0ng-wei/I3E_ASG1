using UnityEngine;
using TMPro;

public class EndZone : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject requirementPanel;
    [SerializeField] TextMeshProUGUI requirementText;
    [SerializeField] TextMeshProUGUI endScoreText;
    [SerializeField] TextMeshProUGUI endTimeText;

    [Header("Audio")]
    [SerializeField] private AudioClip winSound;

    [Header("Settings")]
    [SerializeField] int targetPoints = 50;

    void Start()
    {
        if (endScreen != null)
            endScreen.SetActive(false);

        if (requirementText != null)
            requirementPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered end zone with " + other.GetComponent<Player>().points + " points");

            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (player.points >= targetPoints)
            {
                ShowEndScreen(player);
            }
            else
            {
                if (requirementText != null)
                {
                    if (requirementPanel != null)
                    requirementPanel.SetActive(true);
                    
                    if (requirementText != null)
                    requirementText.text = "Achieve " + targetPoints + " points to win";
        }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requirementPanel != null)
                requirementPanel.SetActive(false);
        }
    }

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