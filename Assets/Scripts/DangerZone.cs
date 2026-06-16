using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public enum ZoneType { PoisonGas, Lava, Water }

    [Header("Zone Settings")]
    public ZoneType zoneType;

    [Header("Audio")]
    [SerializeField] AudioClip zoneSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DangerZone trigger entered by: " + other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered danger zone, playing sound");
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (audioSource != null && zoneSound != null)
            {
                audioSource.clip = zoneSound;
                audioSource.loop = true;
                audioSource.Play();
            }

            else
            {
                Debug.Log("AudioSource: " + (audioSource != null) + " ZoneSound: " + (zoneSound != null));
            }

            switch (zoneType)
            {
                case ZoneType.PoisonGas:
                    player.EnterPoisonGas();
                    break;
                case ZoneType.Lava:
                    player.EnterLava();
                    break;
                case ZoneType.Water:
                    player.InstantDeath();
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (audioSource != null)
                audioSource.Stop();

            switch (zoneType)
            {
                case ZoneType.PoisonGas:
                    player.ExitPoisonGas();
                    break;
                case ZoneType.Lava:
                    player.ExitLava();
                    break;
            }
        }
    }
}