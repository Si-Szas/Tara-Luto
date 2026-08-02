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
        beginTapsilogButton.onClick.AddListener(LoadTapsilogLevel);
        beginLumpiaButton.onClick.AddListener(LoadLumpiaLevel);
        beginAdoboButton.onClick.AddListener(LoadAdoboLevel);
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
    }

    public void LoadMainGameScreen()
    {
        SceneManager.LoadScene("2_GAME");
    }

}
