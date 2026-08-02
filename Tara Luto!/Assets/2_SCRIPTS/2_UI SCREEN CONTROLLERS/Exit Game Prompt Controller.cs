using UnityEngine;
using UnityEngine.UIElements;

public class ExitGamePromptController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject exitGamePromptParent;

    private Button closeExitPromptButton;
    private Button confirmExitButton;

    private void OnEnable()
    {
        if (!uiDocument)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        closeExitPromptButton = uiDocument.rootVisualElement.Q<Button>("CloseExitPromptButton");
        confirmExitButton = uiDocument.rootVisualElement.Q<Button>("ConfirmExitButton");
        
        SetUpExitGamePrompt();
    }

    private void SetUpExitGamePrompt()
    {
        confirmExitButton.clicked += ExitGame;
        closeExitPromptButton.clicked += CloseExitPrompt;
    }

    private void CloseExitPrompt()
    {
        Debug.Log("Closed Exit Prompt");
        AudioManager.Instance.PlayButtonClickSFX();
        exitGamePromptParent.SetActive(false);
    }
   
    private void ExitGame()
    {
        Debug.Log("Exiting Game");
        AudioManager.Instance.PlayCloseMenuSFX();
        Application.Quit();
    }

}
