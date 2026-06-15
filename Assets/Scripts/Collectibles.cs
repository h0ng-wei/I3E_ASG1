using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public Vector3 _rotation;
    public float _speed;
    public GameObject onCollectEffect;

    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }

    public void Collect()
    {
        Instantiate(onCollectEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}