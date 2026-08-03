using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Make it a singleton
    public static AudioManager Instance { get; private set; }

    [Header("Audio Configurations")]
    //Simultaneous sfx allowed
    [SerializeField] private int audioSourceSimultaneousMax = 10;
    [SerializeField] private AudioSource sceneBGMSource;
    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip mistakeSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip stars3SFX;
    [SerializeField] private AudioClip stars2SFX;
    [SerializeField] private AudioClip stars1SFX;
    [SerializeField] private AudioClip stars0SFX;
    [SerializeField] private AudioClip affirmativeSFX;
    //[SerializeField] private AudioClip closeMenuSFX;

    private AudioSource[] sfxSources;
    private Coroutine fadeCoroutine;


    //Singleton pattern so that any object can just call the manager
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudio()
    {
        //BGM SET UP
        sceneBGMSource = gameObject.AddComponent<AudioSource>();
        sceneBGMSource.playOnAwake = false;
        sceneBGMSource.loop = true; 

        //SFX SET UP
        sfxSources = new AudioSource[audioSourceSimultaneousMax];

        for (int i = 0; i < audioSourceSimultaneousMax; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            sfxSources[i] = source;
        }
    }

    public void PlayBGM(AudioClip musicClip, float volume = 0.5f)
    {
        if (musicClip == null || sceneBGMSource.clip == musicClip) return;

        sceneBGMSource.clip = musicClip;
        sceneBGMSource.volume = volume;
        sceneBGMSource.Play();
    }

    public void ChangeBGMWithFade(AudioClip newMusicClip, float fadeDuration = 1.0f, float targetVolume = 0.5f)
    {
        if (newMusicClip == null || sceneBGMSource.clip == newMusicClip) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeBGMCoroutine(newMusicClip, fadeDuration, targetVolume));
    }

    private IEnumerator FadeBGMCoroutine(AudioClip newClip, float duration, float targetVolume)
    {
        float startVolume = sceneBGMSource.volume;

        // Fade Out current music
        if (sceneBGMSource.isPlaying)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                sceneBGMSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
                yield return null;
            }
        }

        // Swap out the track
        sceneBGMSource.clip = newClip;
        sceneBGMSource.Play();

        // Fade In new music
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            sceneBGMSource.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }

        sceneBGMSource.volume = targetVolume;
    }

    public void StopBGM()
    {
        sceneBGMSource.Stop();
    }

    public void PauseBGM()
    {
        sceneBGMSource.Pause();
    }

    public void UnpauseBGM()
    {
        sceneBGMSource.UnPause();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        if (clip == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.volume = volume;
            availableSource.pitch = pitch;
            availableSource.loop = loop;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayCorrectSFX()
    {
        if (correctSFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = correctSFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayMistakeSFX()
    {
        if (mistakeSFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = mistakeSFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }
    public void PlayButtonClickSFX()
    {
        if (buttonClickSFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = buttonClickSFX;
            availableSource.volume = 1.0f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayPerfectSFX()
    {
        if (stars3SFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = stars3SFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayStars2SFX()
    {
        if (stars2SFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = stars2SFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayStars1SFX()
    {
        if (stars1SFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = stars1SFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayStars0SFX()
    {
        if (stars0SFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = stars0SFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayAffirmativeVoiceSFX()
    {
        if (affirmativeSFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = affirmativeSFX;
            availableSource.volume = 0.75f;
            availableSource.pitch = 1.0f;
            availableSource.loop = false;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void StopSFX(AudioClip clipToStop, float fadeDuration = 0.3f, bool enableFadeOut = true)
    {
        if (clipToStop == null) return;

        AudioSource[] sources = GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            if (source.isPlaying && source.clip == clipToStop)
            {
                //Fade out the sfx
                if (enableFadeOut) { 
                    StartCoroutine(FadeOutAndStopCoroutine(source, fadeDuration));
                } else {
                    source.Stop();
                    source.clip = null;
                }
            }
        }
    }

    private IEnumerator FadeOutAndStopCoroutine(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            if (source == null) yield break;

            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
            yield return null;
        }

        if (source != null)
        {
            source.Stop();
            source.clip = null;
            source.volume = startVolume; 
        }
    }

    //public void PlayCloseMenuSFX()
    //{
    //    if (closeMenuSFX == null) return;

    //    AudioSource availableSource = GetAvailableSource();

    //    if (availableSource != null)
    //    {
    //        availableSource.clip = closeMenuSFX;
    //        availableSource.volume = 1.0f;
    //        availableSource.pitch = 1.0f;
    //        availableSource.Play();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Maximum simultaneous sounds reached!");
    //    }
    //}

    private AudioSource GetAvailableSource()
    {
        for (int i = 0; i < sfxSources.Length; i++)
        {
            if (!sfxSources[i].isPlaying)
            {
                return sfxSources[i];
            }
        }

        return null;
    }
}
