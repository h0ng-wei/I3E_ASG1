using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public enum ZoneType { PoisonGas, Lava, Water }

    [Header("Zone Settings")]
    public ZoneType zoneType;

    [Header("Audio")]
    [SerializeField] private AudioClip enterSound;
    [SerializeField] private AudioSource loopingAudioSource; // for continuous sounds

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (enterSound != null)
                AudioSource.PlayClipAtPoint(enterSound, transform.position);

            if (loopingAudioSource != null)
                loopingAudioSource.Play();

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

            // stop looping sound
            if (loopingAudioSource != null)
                loopingAudioSource.Stop();

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