using UnityEngine;
using System.Collections;

public class ZombieBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 2.0f;
    public GameObject explosionFX;

    private float _moveDuration; // Time the zombie moves
    private float _stopDuration; // Time the zombie stops
    private float _timeSinceLastMove;
    private float _randomRotationAngle;

    private bool _isMoving = true;
    private Vector3 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetRandomMovement();
        StartCoroutine(HandleMovement());
    }

    private void FixedUpdate()
    {
        if (_isMoving) {
            // Calculate the movement direction based on the random rotation angle
            _moveDirection = Quaternion.Euler(0, _randomRotationAngle, 0) * Vector3.forward;
            _rb.velocity = new Vector3(_moveDirection.x * speed, _rb.velocity.y, _moveDirection.z * speed);

            // Smoothly rotate the zombie to face the movement direction
            RotateZombieToFaceDirection(_moveDirection);
        } else {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0); // Stop movement
        }

        if (transform.position.y < -10.0f) Destroy(gameObject); // Destroy if the zombie falls off the platform
    }

    private void RotateZombieToFaceDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Smoothly rotate towards the target direction over time
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
    }

    private void SetRandomMovement()
    {
        _moveDuration = Random.Range(2.0f, 5.0f); // Random time for moving
        _stopDuration = Random.Range(1.0f, 3.0f); // Random time for stopping
        _randomRotationAngle = Random.Range(0, 360); // Random rotation angle
    }

    private IEnumerator HandleMovement()
    {
        while (true) {
            _isMoving = true;
            yield return new WaitForSeconds(_moveDuration); // Move for a random duration

            _isMoving = false;
            yield return new WaitForSeconds(_stopDuration); // Stop for a random duration

            SetRandomMovement(); // Set new random values for the next cycle
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PlayerTag")) return;
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlayZombieSound();
        Destroy(gameObject);
        // if (collision.gameObject.transform.Find("tank_02_c") == null) return;
        // Destroy(collision.gameObject.transform.Find("tank_02_c").gameObject);
        // collision.gameObject.GetComponent<Rigidbody>().mass = 1000;
    }
}
