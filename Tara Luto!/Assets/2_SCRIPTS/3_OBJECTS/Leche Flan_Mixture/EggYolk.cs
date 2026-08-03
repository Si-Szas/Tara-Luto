using UnityEngine;
using UnityEngine.InputSystem;

public class EggYolk : MonoBehaviour
{
    [SerializeField] private AudioClip yolkPickupSFX;
    [SerializeField] private AudioClip yolkDropSFX;

    private CircleCollider2D yolkCollider;
    private bool isBeingHeld = false;
    private bool cursorHere = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yolkCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pointer.current == null) return;

        Vector2 pos = Pointer.current.position.ReadValue();
        CheckCollider(pos);

        //only when clicked is sfx playing
        if (Pointer.current.press.wasPressedThisFrame && cursorHere)
        {
            isBeingHeld = true;
            AudioManager.Instance.PlaySFX(yolkPickupSFX, 0.5f);
        }

        //drop yolk
        if (Pointer.current.press.wasReleasedThisFrame && isBeingHeld)
        {
            isBeingHeld = false;
            AudioManager.Instance.PlaySFX(yolkDropSFX, 0.5f);
        }

        if (isBeingHeld)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1f));
            transform.position = worldPos;
        }
    }

    private void CheckCollider(Vector2 pos)
    {
        if (yolkCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1f))))
        {
            cursorHere = true;
        }
        else
        {
            cursorHere = false;
        }
    }
}