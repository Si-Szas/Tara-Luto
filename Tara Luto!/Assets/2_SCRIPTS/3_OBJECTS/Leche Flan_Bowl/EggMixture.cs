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

    [SerializeField] private float mixThreshold = 0.5f;

    [SerializeField] AudioClip mixingSFX;
    private bool isMixing = false;

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
            AudioManager.Instance.StopSFX(mixingSFX);
        }

        if (isTracking && isMoving)
        {
            animator.SetBool("isMixing", true);

            if (progressBar.fillAmount < mixThreshold)
            {
                progressBar.fillAmount += 0.05f;

                if(!isMixing)
                {
                    isMixing = true;
                    AudioManager.Instance.PlaySFX(mixingSFX, 0.75f, 1.0f, true);
                }

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
                AudioManager.Instance.StopSFX(mixingSFX);
                nextPhase.SetActive(true);
                animator.SetBool("isMixing", false);
                spriteRenderer.sprite = sprite1;
                transform.parent.gameObject.SetActive(false);
            }
        } else
        {
            AudioManager.Instance.StopSFX(mixingSFX);
            isMixing = false;
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
