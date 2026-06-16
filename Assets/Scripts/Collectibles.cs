// =============================================================================
// Collectibles.cs
// Handles collectible objects that rotate in place and are destroyed when
// collected, playing a sound and spawning a visual effect on collection.
// =============================================================================

using UnityEngine;

public class Collectibles : MonoBehaviour
{
    /// <summary>The axis and direction the collectible rotates.</summary>
    public Vector3 _rotation;

    /// <summary>The speed at which the collectible rotates.</summary>
    public float _speed;

    /// <summary>The particle effect spawned when the collectible is collected.</summary>
    public GameObject onCollectEffect;

    /// <summary>The number of points this collectible gives when collected.</summary>
    public int points = 1;

    /// <summary>The sound played when the collectible is collected.</summary>
    [SerializeField] private AudioClip collectSound;

    /// <summary>Rotates the collectible every frame.</summary>
    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }

    /// <summary>Plays a sound, spawns an effect and destroys this collectible when collected.</summary>
    public void Collect()
    {
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        Instantiate(onCollectEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}