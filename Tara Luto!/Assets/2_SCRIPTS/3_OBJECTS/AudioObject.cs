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
        if(playOnStart) {
            audioManager.PlaySFX(sfx, volume, pitch, isLoopable);
        }
    }

    private void OnEnable()
    {
        audioManager.PlaySFX(sfx, volume, pitch, isLoopable);
    }

    private void OnDisable()
    {
        audioManager.StopSFX(sfx);
    }
}
