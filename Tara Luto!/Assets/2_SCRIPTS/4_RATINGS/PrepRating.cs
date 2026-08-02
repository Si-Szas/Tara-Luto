using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PrepRating : MonoBehaviour
{
    [SerializeField] Sprite litStar;
    [SerializeField] RatingManager ratingManager;
    [SerializeField] GameObject starsParent;

    private List<UnityEngine.UI.Image> stars = new List<UnityEngine.UI.Image>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
    }

    public void LightStars(UnityEngine.UI.Image endProgress)
    {
        Debug.Log("Progress val: " + endProgress.fillAmount);
        int starCount = 0;

        if (endProgress.fillAmount >= 0.3)
            starCount++;
        if (endProgress.fillAmount >= 0.6)
            starCount++;
        if (endProgress.fillAmount == 1.0)
            starCount++;

        Debug.Log("Star val: " + starCount);

        if (stars == null || stars.Count == 0)
        {
            stars.Clear();
            stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
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
