using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button beginTapsilogButton;
    [SerializeField] private Button beginLumpiaButton;
    [SerializeField] private Button beginAdoboButton;
    [SerializeField] private Button beginLecheFlanButton;

    private void Awake()
    {
        if(beginTapsilogButton != null)
            beginTapsilogButton.onClick.AddListener(LoadTapsilogLevel);
        if (beginLumpiaButton != null)
            beginLumpiaButton.onClick.AddListener(LoadLumpiaLevel);
        if (beginAdoboButton != null)
            beginAdoboButton.onClick.AddListener(LoadAdoboLevel);
        if (beginLecheFlanButton != null)
            beginLecheFlanButton.onClick.AddListener(LoadLecheFlanLevel);
    }
    private void LoadTapsilogLevel()
    {
        //PLACE TAPSILOG LEVEL HERE
        Debug.Log("Loaded tapsilog level");
    }

    private void LoadLumpiaLevel()
    {
        //PLACE LUMPIA LEVEL HERE
        Debug.Log("Loaded lumpia level");
    }
    private void LoadAdoboLevel()
    {
        //PLACE ADOBO LEVEL HERE
        Debug.Log("Loaded adobo level");
    }

    private void LoadLecheFlanLevel()
    {
        SceneManager.LoadScene("6_LECHE FLAN");
        AudioManager.Instance.PlayButtonClickSFX();
    }

    public void LoadMainGameScreen()
    {
        SceneManager.LoadScene("2_GAME");
        AudioManager.Instance.PlayButtonClickSFX();
    }

}
