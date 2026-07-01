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
        exitGameButton = uiDocument.rootVisualElement.Q<Button>("exitGameButton");
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
    }

    private void StartGame()
    {
        Debug.Log("Started Game");
        SceneManager.LoadScene(sceneToStart);
    }

    private void ViewSettings()
    {
        Debug.Log("Viewing Settings");
        settingsObject.SetActive(true);
    }

    private void ViewCredits()
    {
        Debug.Log("Viewing Credits");
        creditsObject.SetActive(true);
    }

    private void ExitGame()
    {
        Debug.Log("Exited Game");
        exitGameObject.SetActive(true);
    }

}
