using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject settingsObject;
    [SerializeField] private GameObject creditsObject;
    [SerializeField] private GameObject exitGameObject;
    [SerializeField] private string sceneToStart;

    [Header("BGM")]
    [SerializeField] private AudioClip mainMenuBGM;

    private Button startGameButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button exitGameButton;

    private void Awake()
    {
        if (!uiDocument)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        startGameButton = uiDocument.rootVisualElement.Q<Button>("StartGameButton");
        settingsButton = uiDocument.rootVisualElement.Q<Button>("SettingsButton");
        creditsButton = uiDocument.rootVisualElement.Q<Button>("CreditsButton");
        exitGameButton = uiDocument.rootVisualElement.Q<Button>("ExitGameButton");
    }

    private void Start()
    {
        if (sceneToStart == "")
        {
            sceneToStart = "2_GAME";
        }

        startGameButton.clicked += StartGame;
        settingsButton.clicked += ViewSettings;
        creditsButton.clicked += ViewCredits;
        exitGameButton.clicked += ExitGame;

        AudioManager.Instance.PlayBGM(mainMenuBGM, 0.25f);
    }

    private void StartGame()
    {
        Debug.Log("Started Game");
        AudioManager.Instance.PlayButtonClickSFX();
        SceneManager.LoadScene(sceneToStart);
    }

    private void ViewSettings()
    {
        Debug.Log("Viewing Settings");
        AudioManager.Instance.PlayButtonClickSFX();
        settingsObject.SetActive(true);
    }

    private void ViewCredits()
    {
        Debug.Log("Viewing Credits");
        AudioManager.Instance.PlayButtonClickSFX();
        creditsObject.SetActive(true);
    }

    private void ExitGame()
    {
        Debug.Log("Viewing Exit Game Prompt");
        AudioManager.Instance.PlayButtonClickSFX();
        exitGameObject.SetActive(true);
    }

}
