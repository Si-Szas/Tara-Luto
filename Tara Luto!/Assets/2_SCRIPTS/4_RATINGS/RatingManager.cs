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

    private bool totalled = false;

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

        for (int i = 0; i < totalStars; i++)
        {
            stars[i].sprite = litStar;  
        }

        totalled = true;
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
}
