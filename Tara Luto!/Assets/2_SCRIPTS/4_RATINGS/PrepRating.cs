using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class PrepRating : MonoBehaviour
{
    [SerializeField] Sprite litStar;
    [SerializeField] RatingManager ratingManager;
    [SerializeField] GameObject starsParent;
    //Default
    [SerializeField] GameObject textObject;
    TextMeshProUGUI feedbackText;

    private List<UnityEngine.UI.Image> stars = new List<UnityEngine.UI.Image>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
        feedbackText = textObject.GetComponent<TextMeshProUGUI>();
    }

    public void LightStars(UnityEngine.UI.Image endProgress)
    {
        int starCount = 0;

        if (stars == null || stars.Count == 0 || feedbackText == null)
        {
            stars.Clear();
            PopulateList();
        }

        if (endProgress.fillAmount >= 0.3) 
        {
            feedbackText.text = "Nice try!";
            starCount++;
        }
        if (endProgress.fillAmount >= 0.6)
        {
            feedbackText.text = "Good job! Let's move on to the next step!";
            starCount++;
        }
        if (endProgress.fillAmount == 1.0)
        {
            feedbackText.text = "WOW! Ang galing mo!";
            starCount++;
        }

        for (int i = 0; i < starCount; i++)
        {
            stars[i].sprite = litStar;
        }

        ratingManager.AddToOverallRating(starCount);
    }

    private void PopulateList()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
    }
}
