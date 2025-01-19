using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject explosionFX;
    private Vector3 _spawnPosition;

    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(Vector3.forward * 100);
        _spawnPosition = transform.position;
    }
    
    private void Update()
    {
        _rb.AddForce(Vector3.forward * 100);
        if (Vector3.Distance(_spawnPosition, transform.position) > 150.0f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("EnemyTag")) return;
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlayExplosionSound();
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
