using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] float volume = 1.0f;
    [SerializeField] float pitch = 1.0f;
    [SerializeField] bool isLoopable = false;

    private void OnEnable()
    {
        AudioManager.Instance.PlaySFX(sfx, volume, pitch, isLoopable);
    }

    private void OnDisable()
    {
        AudioManager.Instance.StopSFX(sfx);
    }
}
