using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip JumpAudioClip;
    public AudioClip PlatformAudioClip;
    public AudioClip FailAudioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerJumped += JumpSound;
        Platform.OnPlayerLanded += PlatformSound;
        PlayerController.OnPlayerDied += FailSound;
    }

    private void JumpSound()
    {
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(JumpAudioClip);
    }

    private void PlatformSound()
    {
        _audioSource.volume = 0.1f;
        _audioSource.PlayOneShot(PlatformAudioClip);
    }

    private void FailSound()
    {
        _audioSource.volume = 0.2f;
        _audioSource.PlayOneShot(FailAudioClip);
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerJumped -= JumpSound;
        Platform.OnPlayerLanded -= PlatformSound;
        PlayerController.OnPlayerDied -= FailSound;
    }
}
