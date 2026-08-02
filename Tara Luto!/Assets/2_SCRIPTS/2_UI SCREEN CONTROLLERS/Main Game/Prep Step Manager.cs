using UnityEngine;
using UnityEngine.UI;

public class PrepStepManager : MonoBehaviour
{
    [Header("ProceedScreen")]
    [SerializeField] GameObject proceedScreen;

    [Header("Timer")]
    [SerializeField] Image timerFill;
    [SerializeField] GameObject timerHand;

    [Header("Completion")]
    [SerializeField] Image barFill;

    private bool isCounting = false;
    private float timer = 0f;
    private float timerMax = 30f;

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
            timerFill.fillAmount -= 0.033f * Time.deltaTime;
            barFill.fillAmount += 0.033f * Time.deltaTime;
            timerHand.transform.rotation *= Quaternion.Euler(0f, 0f, 12f * Time.deltaTime);
        }

        if (timer > timerMax)
        {
            proceedScreen.SetActive(true);
            proceedScreen.GetComponent<PrepRating>().LightStars(barFill);

            timer = 0f;
            timerFill.fillAmount = 1f;
            barFill.fillAmount = 0f;
            timerHand.transform.rotation.Set(0f, 0f, 0f, 1f);
            isCounting = false;

            this.gameObject.SetActive(false);
        }
    }

    public void StartCounting()
    {
        isCounting = true;
    }
}
