using UnityEngine;
using UnityEngine.InputSystem;

public class EggYolk : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    InputAction tap;
    InputAction drag;

    private CircleCollider2D yolkCollider;
    private bool isBeingHeld = false;
    private bool cursorHere = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tap = playerInput.actions.FindAction("Attack");
        drag = playerInput.actions.FindAction("Drag");

        yolkCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = drag.ReadValue<Vector2>();
        CheckCollider(pos);

        if (tap.WasPressedThisFrame() && cursorHere)
        {
            isBeingHeld = true;
        }
        if (tap.WasReleasedThisFrame())
        {
            isBeingHeld = false;
        }

        if (isBeingHeld)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, drag.ReadValue<Vector2>().y, 1));
        }
    }

    private void CheckCollider(Vector2 pos)
    {
        if (yolkCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(pos.x, drag.ReadValue<Vector2>().y, 1))))
        {
            cursorHere = true;
        } else
        {
            cursorHere = false;
        }
    }

}
