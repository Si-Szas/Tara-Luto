using UnityEngine;

public static class StarManager
{
    static public int lecheFlanStars = 0;

    static public void SaveStarCount()
    {
        PlayerPrefs.SetInt("LecheFlanStars", lecheFlanStars);
    }

    static public void LoadStarCount()
    {
        lecheFlanStars = PlayerPrefs.GetInt("LecheFlanStars");
    }
}
