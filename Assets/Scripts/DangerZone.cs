// =============================================================================
// DangerZone.cs
// Detects when the player enters a danger zone and applies the appropriate
// damage type while playing a looping audio effect inside the zone.
// =============================================================================

using UnityEngine;

public class DangerZone : MonoBehaviour
{
    /// <summary>The type of danger zone this object represents.</summary>
    public enum ZoneType { PoisonGas, Lava, Water }

    [Header("Zone Settings")]
    /// <summary>The zone type set in the Inspector to determine what damage is applied.</summary>
    public ZoneType zoneType;

    [Header("Audio")]
    /// <summary>The sound played on loop when the player is inside the zone.</summary>
    [SerializeField] AudioClip zoneSound;

    /// <summary>The AudioSource component used to play zone audio.</summary>
    AudioSource audioSource;

    /// <summary>Gets the AudioSource component on scene load.</summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Applies damage and plays audio when the player enters the zone.</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (audioSource != null && zoneSound != null)
            {
                audioSource.clip = zoneSound;
                audioSource.loop = true;
                audioSource.spatialBlend = 0f;
                audioSource.Play();
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

    /// <summary>Stops damage and audio when the player exits the zone.</summary>
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