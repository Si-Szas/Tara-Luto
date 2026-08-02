using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EggMixture : MonoBehaviour
{
    [Header("Other Sprites")]
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] Sprite sprite3;

    [SerializeField] Image progressBar;
    [SerializeField] GameObject nextPhase;

    private bool isTracking = false;
    private bool isMoving = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 pos = Pointer.current.position.ReadValue();
        CheckIfMoved();

        if (Pointer.current.press.isPressed)
        {
            isTracking = true;
        }
        if (Pointer.current.press.wasReleasedThisFrame)
        {
            animator.SetBool("isMixing", false);
            isTracking = false;
        }

        if (isTracking && isMoving)
        {
            animator.SetBool("isMixing", true);

            if (progressBar.fillAmount < 0.5)
            {
                progressBar.fillAmount += 0.0002f;
                if (progressBar.fillAmount >= 0.16f && progressBar.fillAmount < 0.32)
                {
                    Debug.Log("sprite 2");
                    spriteRenderer.sprite = sprite2;
                }
                if (progressBar.fillAmount >= 0.32f && progressBar.fillAmount < 0.5)
                    spriteRenderer.sprite = sprite3;
            }
            else
            {
                nextPhase.SetActive(true);
                animator.SetBool("isMixing", false);
                spriteRenderer.sprite = sprite1;
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void CheckIfMoved()
    {
        Vector2 pointerDelta = Pointer.current.delta.ReadValue();

        if (Pointer.current != null && pointerDelta != Vector2.zero)
        {
            isMoving = true;
        } else
        {
            isTracking = false;
        }
    }
   
}
