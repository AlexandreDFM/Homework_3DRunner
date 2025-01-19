using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource _audioBackgroundSource;
    private AudioSource _audioEffectSource;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip[] zombieSounds;
    [SerializeField] private AudioClip backgroundMusic;
    // Start is called before the first frame update
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _audioBackgroundSource = gameObject.GetComponent<AudioSource>();
        _audioEffectSource = gameObject.AddComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayExplosionSound()
    {
        _audioEffectSource.PlayOneShot(explosionSound);
    }
    
    public void PlayZombieSound()
    {
        _audioEffectSource.PlayOneShot(zombieSounds[Random.Range(0, zombieSounds.Length)]);
    }

    private void PlayBackgroundMusic()
    {
        _audioBackgroundSource.clip = backgroundMusic;
        _audioBackgroundSource.loop = true;
        _audioBackgroundSource.Play();
    }
}