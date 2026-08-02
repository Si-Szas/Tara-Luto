using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    [Header("Object Options")]
    [SerializeField] private bool isDestroyable;

    [Header("Sprite Options")]
    [SerializeField] private Sprite spriteChangeAfterDrag;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    private Camera mainCamera;
    private bool isDragging = true;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Pointer.current == null) return;

        //Drag object
        if (isDragging && Pointer.current.press.isPressed)
        {
            //Get world pos
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            transform.position = worldPos;
        }

        // Released (if they release the object before it was placed on bowl make the egg crack??
        if (isDragging && Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
            //Broadcast that egg was dropped UNLESS it was dropped on the 
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with has the correct tag
        if (other.CompareTag("Collide With"))
        {
            mySpriteRenderer.sprite = spriteChangeAfterDrag;
            Debug.Log("GOODBYE EGG");

            // Destroy this draggable game object instantly
            Destroy(gameObject);
        }
    }

}