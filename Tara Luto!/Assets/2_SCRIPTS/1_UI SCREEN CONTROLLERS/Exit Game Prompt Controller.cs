using UnityEngine;
using UnityEngine.UIElements;

public class ExitGamePromptController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject exitGamePromptParent;

    private Button closeExitPrompt;
    private Button confirmExit;

    private void OnEnable()
    {
        if (!uiDocument)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        closeExitPrompt = uiDocument.rootVisualElement.Q<Button>("CloseExitPromptButton");
        confirmExit = uiDocument.rootVisualElement.Q<Button>("ConfirmExitButton");
        
        SetUpExitGamePrompt();
    }

    private void SetUpExitGamePrompt()
    {
        confirmExit.clicked += ExitGame;
        closeExitPrompt.clicked += CloseExitPrompt;
    }

    private void CloseExitPrompt()
    {
        Debug.Log("Closed Exit Prompt");
        exitGamePromptParent.SetActive(false);
    }
   
    private void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

}
