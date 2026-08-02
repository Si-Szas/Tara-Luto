using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] private AudioClip recipeBGM;
     void Start()
    {
        AudioManager.Instance.PlayBGM(recipeBGM, 0.15f);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("2_GAME");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
