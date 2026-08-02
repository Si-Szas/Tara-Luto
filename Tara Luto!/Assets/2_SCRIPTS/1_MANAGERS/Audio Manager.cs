using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Make it a singleton
    public static AudioManager Instance { get; private set; }

    [Header("Audio Configurations")]
    //Simultaneous sfx allowed
    [SerializeField] private int audioSourceSimultaneousMax = 10;
    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip mistakeSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip closeMenuSFX;

    private AudioSource[] sfxSources;


    //Singleton pattern so that any object can just call the manager
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSimultaneousMax();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSimultaneousMax()
    {
        sfxSources = new AudioSource[audioSourceSimultaneousMax];

        for (int i = 0; i < audioSourceSimultaneousMax; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            sfxSources[i] = source;
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.volume = volume;
            availableSource.pitch = pitch;
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
            availableSource.volume = 1.0f;
            availableSource.pitch = 1.0f;
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
            availableSource.volume = 1.0f;
            availableSource.pitch = 1.0f;
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
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

    public void PlayCloseMenuSFX()
    {
        if (closeMenuSFX == null) return;

        AudioSource availableSource = GetAvailableSource();

        if (availableSource != null)
        {
            availableSource.clip = closeMenuSFX;
            availableSource.volume = 1.0f;
            availableSource.pitch = 1.0f;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("Maximum simultaneous sounds reached!");
        }
    }

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
