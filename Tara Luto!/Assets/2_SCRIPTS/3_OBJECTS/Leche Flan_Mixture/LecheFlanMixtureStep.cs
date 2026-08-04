using UnityEngine;
using UnityEngine.UI;

public class LecheFlanMixtureStep : MonoBehaviour
{
    [SerializeField] Image progressBar;
    public int yolksIn = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("col detected");
        if (collision.gameObject.tag == "eggYolk")
        {
            yolksIn++;
            progressBar.fillAmount += 0.167f;
            AudioManager.Instance.PlayCorrectSFX();
        }

        if (yolksIn == 6)
            progressBar.fillAmount = 1.0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("col leave detected");
        if (collision.gameObject.tag == "eggYolk")
        {
            yolksIn--;
            AudioManager.Instance.PlayMistakeSFX();
            progressBar.fillAmount -= 0.167f;
        }
    }

    private void OnDisable()
    {
        AudioManager.Instance.StopMistakeSFX();
    }
}
