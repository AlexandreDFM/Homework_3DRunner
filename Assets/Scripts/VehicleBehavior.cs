using UnityEngine;

public class VehicleBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    private AudioSource _audioSource;

    public float speed = 2.0f;
    public float moveVertical = 1.0f;
    public float laneChangeInterval = 3.0f;  // Time interval between lane changes
    public float laneChangeAmount = 2.0f;    // Amount of lane change (+2 or -2)
    public float laneChangeSpeed = 2.0f;     // Speed of lane change
    public GameObject explosionFX;

    private Vector3 _targetPosition; // The target position for smooth lane changes
    private float _nextLaneChangeTime;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _nextLaneChangeTime = Time.time + laneChangeInterval; // Schedule first lane change
        _targetPosition = transform.position; // Initial target is the current position
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
        _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, transform.position.z + moveVertical * speed);
        SmoothMoveToTargetLane();
        if (Time.time > _nextLaneChangeTime) {
            SetRandomLaneChange();
            _nextLaneChangeTime = Time.time + laneChangeInterval;
        }
    
        if (transform.position.y < -10.0f) Destroy(gameObject);
    }
    
    // private void FixedUpdate()
    // {
    //     // Vector3 velocity = _rb.velocity;
    //     // velocity.z = moveVertical * speed;
    //     // _rb.velocity = velocity;
    //     
    //     transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    //
    //     SmoothMoveToTargetLane();
    //
    //     if (Time.time > _nextLaneChangeTime)
    //     {
    //         SetRandomLaneChange();
    //         _nextLaneChangeTime = Time.time + laneChangeInterval;
    //     }
    //
    //     if (transform.position.y < -10.0f)
    //         Destroy(gameObject);
    // }


    private void SetRandomLaneChange()
    {
        float randomDirection = Random.Range(0, 2) == 0 ? -laneChangeAmount : laneChangeAmount;
        _targetPosition = new Vector3(Mathf.Clamp(transform.position.x + randomDirection, -2.0f, 2.0f), transform.position.y, _targetPosition.z);
    }

    private void SmoothMoveToTargetLane()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * laneChangeSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PlayerTag")) return;
        TriggerExplosion();
        Destroy(gameObject);
    }

    private void TriggerExplosion()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        SoundManager.instance.PlayExplosionSound();
    }
}
