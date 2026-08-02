using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] private AudioClip recipeBGM;
     void Start()
    {
        AudioManager.Instance.PlayBGM(recipeBGM, 0.15f);
    }
}
