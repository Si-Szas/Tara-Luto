using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject settingsParent;

    private Button closeSettingsButton;

    private void OnEnable()
    {
        if (!uiDocument)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        closeSettingsButton = uiDocument.rootVisualElement.Q<Button>("CloseSettingsButton");

        SetUpSettings();
    }

    private void SetUpSettings()
    {
        closeSettingsButton.clicked += CloseSettings;
    }

    private void CloseSettings()
    {
        Debug.Log("Closed Settings");
        settingsParent.SetActive(false);
    }

}
