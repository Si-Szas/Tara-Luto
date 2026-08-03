using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PrepStepManager prepStepManager;
    [SerializeField] private bool destroyAfterCollide = false;
    [SerializeField] private float lerpSpeed = 5.0f;
    [SerializeField] private bool hasNextPhase = false;
    [SerializeField] private GameObject nextPhaseGameObject;
    [SerializeField] private float progressThreshold;

    [Header("Object to Collide With")]
    [SerializeField] private GameObject objectToCollideWith;

    [Header("Sprite Options")]
    [SerializeField] private bool changeSpriteAfterDrag = false;
    [SerializeField] private Sprite spriteChangeAfterDrag;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    [Header("Progress Tracker")]
    [SerializeField] Image barFill;

    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 startPosition;
    private Collider2D myCollider;
    private bool lerpBack = false;
    private bool goToNextPhase = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();

        //If we dont want object to be destroyed after it collides, then we let it lerp back
        if (!destroyAfterCollide)
        {
            startPosition = transform.position;
        }
    }

    void Update()
    {
        if (Pointer.current == null) return;

        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(mainCamera.transform.position.z)));
        worldPos.z = 0f;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider == myCollider)
            {
                isDragging = true;

                if (!destroyAfterCollide) 
                { 
                    lerpBack = false; 
                }
            }
        }

        if (isDragging && Pointer.current.press.isPressed)
        {
            transform.position = worldPos;
        }

        if (isDragging && Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
            CheckAndDestroyDraggable();
        }

        if (lerpBack && !isDragging && !destroyAfterCollide)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * lerpSpeed);

            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                transform.position = startPosition;
                lerpBack = false;
            }
        }

        if(goToNextPhase)
        {
            nextPhaseGameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
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
                 
                AudioManager.Instance.PlayCorrectSFX();
                Debug.Log("Hit object to collide with!");

                if(!destroyAfterCollide)
                {
                    lerpBack = false;
                    barFill.fillAmount += 0.25f;
                } else {
                    barFill.fillAmount += 0.167f;
                }

                if (hasNextPhase)
                {
                    goToNextPhase = true;
                }
            }
            else //Egg was not placed in the bowl (released)
            {
                prepStepManager.DecreaseTimer();
                AudioManager.Instance.PlayMistakeSFX();
                Debug.Log("Dropped draggable :(");

                if (!destroyAfterCollide)
                {
                    lerpBack = true;
                }
            }

            AudioManager.Instance.PlaySFX(GetComponent<AudioSource>().clip);

            if (destroyAfterCollide)
            {
                Destroy(gameObject, 1.0f);
            }

            if(changeSpriteAfterDrag)
            {
                mySpriteRenderer.sprite = spriteChangeAfterDrag;
            }
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