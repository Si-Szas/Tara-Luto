using UnityEngine;

public class CollidedWith : MonoBehaviour
{
    [Header("Sprites to Change to")]
    [SerializeField] private Sprite[] gameSprites;

    [Header("Increments")]
    [SerializeField] private int increment = 2;
    private int numberOfTimesCollidedWith = 0;
    private int currentSpriteIndex = 0;

    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AddCounter()
    {
        Debug.Log(numberOfTimesCollidedWith);
        numberOfTimesCollidedWith++;

        if (numberOfTimesCollidedWith % increment == 0 && currentSpriteIndex < gameSprites.Length)
        {
            mySpriteRenderer.sprite = gameSprites[currentSpriteIndex];

            currentSpriteIndex++;
        }
    }
}