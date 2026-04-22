
using UnityEngine;

public class SFXManager : MonoBehaviour
{


    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("SFX Sounds")]
    [SerializeField] private AudioClip meatSFX;
    [SerializeField] private AudioClip cutIngredientsSFX;
    [SerializeField] private AudioClip hammerBreakSFX;
    [SerializeField] private AudioClip keySFX;
    [SerializeField] private AudioClip doorSFX;
    [SerializeField] private AudioClip clientHappySFX;
    [SerializeField] private AudioClip clientAngrySFX;
    [SerializeField] private AudioClip buttonClickSFX;


    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip intenseMusic;
    [SerializeField] private AudioClip BadEndingMusic;
    [SerializeField] private AudioClip GoodEndingMusic;
    [SerializeField] private float musicTransitionSpeed = 1f;

    private float musicIntensity = 0f;

    void Start()
    {
        if (musicAudioSource != null && backgroundMusic != null)
        {
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
    }

    void Update()
    {
        // Actualizar intensidad de la música
        if (musicAudioSource != null && intenseMusic != null)
        {
            musicAudioSource.pitch = Mathf.Lerp(1f, 1.5f, musicIntensity);
            musicIntensity = Mathf.Max(0, musicIntensity - Time.deltaTime * musicTransitionSpeed);
        }
    }
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
    public void PlayClientHappySFX()
    {
        PlaySFX(clientHappySFX);
    }

    public void PlayButtonClickSFX()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayClientAngrySFX()
    {
        PlaySFX(clientAngrySFX);
    }
    public void PlayMeatSFX()
    {
        PlaySFX(meatSFX);
    }

    public void PlayCutIngredientsSFX()
    {
        PlaySFX(cutIngredientsSFX);
    }

    public void PlayHammerBreakSFX()
    {
        PlaySFX(hammerBreakSFX);
    }

    public void PlayKeySFX()
    {
        PlaySFX(keySFX);
    }

    public void PlayDoorSFX()
    {
        PlaySFX(doorSFX);
    }

    public void IntensifyMusic()
    {
        musicIntensity = 1f;
        Debug.Log("Música intensificada");
    }

    public void NormalizeMusic()
    {
        musicIntensity = 0f;
        Debug.Log("Música normalizada");
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxAudioSource == null)
        {
            Debug.LogError("SFX AudioSource no está asignado");
            return;
        }

        sfxAudioSource.PlayOneShot(clip);
    }
}