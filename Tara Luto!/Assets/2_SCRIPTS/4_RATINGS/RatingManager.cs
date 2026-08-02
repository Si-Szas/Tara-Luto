using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    [SerializeField] int total = 2;
    [SerializeField] Sprite litStar;
    [SerializeField] GameObject starsParent;

    private List<Image> stars = new List<Image>();

    // Private vars
    [SerializeField]  int overallRating = 0;

    [SerializeField] private bool totalled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<Image>());
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

        totalled = true;
        StarManager.lecheFlanStars = totalStars;
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
