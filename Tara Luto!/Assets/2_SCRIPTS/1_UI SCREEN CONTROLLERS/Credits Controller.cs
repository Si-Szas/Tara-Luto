using UnityEngine;
using UnityEngine.UIElements;

public class CreditsController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject creditsParent;

    private Button closeCreditsButton;

    private void OnEnable()
    {
        if (!uiDocument)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        closeCreditsButton = uiDocument.rootVisualElement.Q<Button>("CloseCreditsButton");

        SetUpCredits();
    }

    private void SetUpCredits()
    {
        closeCreditsButton.clicked += CloseCredits;
    }

    private void CloseCredits()
    {
        Debug.Log("Closed Credits");
        creditsParent.SetActive(false);
    }

}
