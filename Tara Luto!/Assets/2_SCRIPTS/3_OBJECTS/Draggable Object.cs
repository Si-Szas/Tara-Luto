using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PrepStepManager prepStepManager;

    [Header("Object to Collide With")]
    [SerializeField] private GameObject objectToCollideWith;

    [Header("Sprite Options")]
    [SerializeField] private Sprite spriteChangeAfterDrag;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    [Header("Progress Tracker")]
    [SerializeField] Image barFill;

    private Camera mainCamera;
    private bool isDragging = true;
    private Collider2D myCollider;

    private void Awake()
    {
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Pointer.current == null) return;

        if (isDragging && Pointer.current.press.isPressed)
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(mainCamera.transform.position.z)));
            worldPos.z = 0f;
            transform.position = worldPos;
        }

        if (isDragging && Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
            CheckAndDestroyDraggable();
        }
    }

    private void CheckAndDestroyDraggable()
    {
        // Sync transforms when moving
        Physics2D.SyncTransforms();

        Collider2D dragObjectToCollider = objectToCollideWith.GetComponent<Collider2D>();

        if (dragObjectToCollider != null)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = true;

            //Check if the collider overlaps with the object we want it to be colliding with
            if (myCollider.Overlap(contactFilter, new Collider2D[1]) > 0 && IsTargetInOverlap(dragObjectToCollider))
            {
                EventList.TriggerEvent("DraggableObjectCollided");
                barFill.fillAmount += 0.167f;
                AudioManager.Instance.PlayCorrectSFX();
                Debug.Log("Hit object to collide with!");
            }
            else //Egg was not placed in the bowl (released)
            {
                prepStepManager.DecreaseTimer();
                AudioManager.Instance.PlayMistakeSFX();
                Debug.Log("Dropped draggable :(");
            }

            mySpriteRenderer.sprite = spriteChangeAfterDrag;
            AudioManager.Instance.PlaySFX(GetComponent<AudioSource>().clip);
            Destroy(gameObject, 1.0f);
        }

    }

    //Helper function since need to check if if the target is in the overlap when dropped
    private bool IsTargetInOverlap(Collider2D targetCollider)
    {
        //Array bc the overlap cant take one stuff so wtvr
        Collider2D[] results = new Collider2D[5];
        ContactFilter2D filter = new ContactFilter2D { useTriggers = true };
        int count = myCollider.Overlap(filter, results);

        for (int i = 0; i < count; i++)
        {
            if (results[i] == targetCollider) return true;
        }
        return false;
    }
}