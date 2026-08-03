using UnityEngine;
using UnityEngine.UI;

public class WaitFor : MonoBehaviour
{
    [Header("Tracker")]
    [SerializeField] private Image progressBar;
    [SerializeField] private float increaseProgressBy = 0.02f;
    [SerializeField] private float progressThreshold = 0.8f;

    [Header("Phase")]
    //[SerializeField] private bool hasNextPhase;
    [SerializeField] private GameObject nextPhaseGameObject;

    [Header("Audio")]
    [SerializeField] private AudioClip whileWaitingSFX;
    [SerializeField] private AudioClip onEnableSFX;

    private void OnEnable()
    {
        AudioManager.Instance.PlaySFX(onEnableSFX, 0.5f);
        AudioManager.Instance.PlaySFX(whileWaitingSFX, 0.75f, 1.0f, true);
    }
    void Update()
    {
        progressBar.fillAmount += increaseProgressBy * Time.deltaTime;

        if (progressBar.fillAmount >= progressThreshold)
        { 
            nextPhaseGameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        AudioManager.Instance.StopSFX(whileWaitingSFX);
    }
}
