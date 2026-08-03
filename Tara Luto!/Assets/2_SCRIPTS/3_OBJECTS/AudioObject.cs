using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] float volume = 1.0f;
    [SerializeField] float pitch = 1.0f;
    [SerializeField] bool isLoopable = false;
    [SerializeField] bool playOnStart = false;
    
    private AudioManager audioManager = AudioManager.Instance;
 
    private void Start()
    {
        if (AudioManager.Instance == null) return;

        if(playOnStart) {
            audioManager.PlaySFX(sfx, volume, pitch, isLoopable);
        }
    }

    private void OnEnable()
    {
        if(AudioManager.Instance != null) { 
            audioManager.PlaySFX(sfx, volume, pitch, isLoopable);
        }
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
        {
            audioManager.StopSFX(sfx);
        }
    }
}
