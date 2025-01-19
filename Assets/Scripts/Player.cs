using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;

    public float speed = 10.0f;
    public AudioClip idleClip;
    public AudioClip runningClip;
    private AudioSource _audioSource;
    public Transform shootPoint;
    public ParticleSystem shootFX;
    public GameObject bulletPrefab;
    public Transform playerTank;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = idleClip;
        _audioSource.loop = true;
    }

    private void Update()
    {
        if (!playerTank) return;
        if (Input.GetKeyDown(KeyCode.Space)) ShootBullet();
    }

    private void ShootBullet()
    {
        shootFX.Play();
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (!playerTank) return;
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        _rb.velocity = new Vector3(moveHorizontal * speed, _rb.velocity.y, moveVertical * speed);

        if (moveHorizontal != 0 || moveVertical != 0) _audioSource.clip = runningClip;
        else _audioSource.clip = idleClip;

        if (!_audioSource.isPlaying) _audioSource.Play();
    }

}
