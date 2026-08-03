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

    void Update()
    {
        progressBar.fillAmount += increaseProgressBy * Time.deltaTime;

        if (progressBar.fillAmount >= progressThreshold)
        { 
            nextPhaseGameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }

    }
}
