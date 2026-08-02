using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    [SerializeField] Sprite litStar;
    [SerializeField] GameObject starsParent;

    private List<Image> stars = new List<Image>();

    // Private vars
    [SerializeField]  int overallRating = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<Image>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToOverallRating(int count)
    {
        overallRating += count;
    }
}
