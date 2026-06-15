using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public Vector3 _rotation;
    public float _speed;
    public GameObject onCollectEffect;
    public int points = 1;
    [SerializeField] private AudioClip collectSound;

    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }

    public void Collect()
    {
         if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        Instantiate(onCollectEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}