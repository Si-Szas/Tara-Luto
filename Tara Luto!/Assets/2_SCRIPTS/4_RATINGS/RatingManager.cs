using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    [SerializeField] int total = 2;
    [SerializeField] Sprite litStar;
    [SerializeField] GameObject starsParent;
    [SerializeField] GameObject resultTextObject;
    TextMeshProUGUI resultText;

    private List<Image> stars = new List<Image>();

    // Private vars
    [SerializeField]  int overallRating = 0;

    [SerializeField] private bool totalled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<Image>());

        resultText = resultTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!totalled)
        {
            TotalStars();
        }
    }

    public void TotalStars()
    {
        int totalStars = overallRating / total;

        if (stars == null || stars.Count == 0)
        {
            stars.Clear();
            PopulateList();
        }

        for (int i = 0; i < totalStars; i++)
        {
            stars[i].sprite = litStar;  
        }

        switch (totalStars)
        {
            case 0: resultText.text = "TRY AGAIN :(";
                break;
            case 1: resultText.text = "NICE TRY";
                break;
            case 2: resultText.text = "GOOD JOB!";
                break;
            case 3: resultText.text = "PERFECT!!";
                break;
        }

        totalled = true;
        StarManager.lecheFlanStars = totalStars;
        StarManager.SaveStarCount();
    }

    public void AddToOverallRating(int count)
    {
        overallRating += count;
    }

    public void RestartAllRating()
    {
        overallRating = 0;
        totalled = false;
        this.gameObject.SetActive(false);   
    }

    private void PopulateList()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
    }
}
