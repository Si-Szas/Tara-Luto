using System.Collections.Generic;
using UnityEngine;

public class PollStars : MonoBehaviour
{
    [SerializeField] Sprite litStar;
    [SerializeField] GameObject starsParent;

    private List<UnityEngine.UI.Image> stars = new List<UnityEngine.UI.Image>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
    }
    private void OnEnable()
    {
        StarManager.LoadStarCount();

        if (stars == null || stars.Count == 0)
        {
            stars.Clear();
            PopulateList();
        }

        for (int i = 0; i < StarManager.lecheFlanStars; i++)
        {
            stars[i].sprite = litStar;
        }
    }

        private void PopulateList()
    {
        stars.AddRange(starsParent.GetComponentsInChildren<UnityEngine.UI.Image>());
    }

}
