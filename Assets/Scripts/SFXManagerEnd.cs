using UnityEngine;

public class SFXManagerEnd : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    
    [SerializeField] private AudioClip BadEndingMusic;
    [SerializeField] private AudioClip GoodEndingMusic;
    
    public void PlayGoodEndingMusic()
    {
        if (musicAudioSource != null && GoodEndingMusic != null)
        {
            musicAudioSource.clip = GoodEndingMusic;
            musicAudioSource.loop = false;
            musicAudioSource.Play();
        }
    }
    public void PlayBadEndingMusic()
    {
        if (musicAudioSource != null && BadEndingMusic != null)
        {
            musicAudioSource.clip = BadEndingMusic;
            musicAudioSource.loop = false;
            musicAudioSource.Play();
        }
    }
}
