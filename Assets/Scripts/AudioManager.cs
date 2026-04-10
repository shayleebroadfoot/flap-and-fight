using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip powerUpMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioClip enemyHitSfx;
    [SerializeField] private AudioClip heartPickupSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayNormalMusic();
    }

    public void PlayShootSfx()
    {
        sfxSource.PlayOneShot(shootSfx);
    }

    public void PlayEnemyHitSfx()
    {
        sfxSource.PlayOneShot(enemyHitSfx);
    }

    public void PlayHeartPickupSfx()
    {
        sfxSource.PlayOneShot(heartPickupSfx);
    }

    public void PlayNormalMusic()
    {
        if (musicSource.clip == normalMusic && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = normalMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayPowerUpMusic()
    {
        if (musicSource.clip == powerUpMusic && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = powerUpMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}