using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PourableObject : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PrepStepManager prepStepManager;
    [SerializeField] private bool hasNextPhase = false;
    [SerializeField] private GameObject nextPhaseGameObject;

    [Header("Tilt")]
    [SerializeField] private float maxTiltAngle = 60f;
    [SerializeField] private float tiltSensitivity = 5f;
    [SerializeField] private float pourThresholdAngle = 15f;

    [Header("Pour Speed")]
    [SerializeField] private float maxSafePourSpeed = 20f;
    [SerializeField] private float fillRate = 0.05f;
    [SerializeField] private float pourThreshold = 0.25f;

    [Header("Tracker")]
    [SerializeField] private Image barFill;

    [Header("Audio")]
    [SerializeField] private AudioClip pourSFX;

    [Header("Pour Object")]
    [SerializeField] GameObject pourObject;

    private float currentRotation = 0f;
    private float lastFrameRotation = 0f;

    private bool isPouring = false;

    private Animator pourAnimator;

    private void Start()
    {
        pourAnimator = pourObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    void OnDisable()
    {
        if (Accelerometer.current != null)
            InputSystem.DisableDevice(Accelerometer.current);

        AudioManager.Instance.StopSFX(pourSFX);
    }

    void Update()
    {
        float processedTilt = 0f;
        if (Accelerometer.current != null)
        {
            processedTilt = Accelerometer.current.acceleration.ReadValue().x;
        }
        else if (Keyboard.current != null) 
        {
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) processedTilt = 0.5f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) processedTilt = -0.5f;
        }

        //rotate the object to look like it is pouring
        float targetRotation = processedTilt * -maxTiltAngle;
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * tiltSensitivity);

        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
        HandlePourAnimation(currentRotation);

        //calculae speed of pour
        float angleDelta = Mathf.Abs(currentRotation - lastFrameRotation);
        float currentRotationSpeed = angleDelta / Time.deltaTime;

        lastFrameRotation = currentRotation;

        //Fill up progress bar
        HandleProgressivePour(currentRotationSpeed);

        if (hasNextPhase) { 
            if(barFill.fillAmount >= pourThreshold)
            {
                nextPhaseGameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void HandleProgressivePour(float rotationSpeed)
    {
        if (currentRotation >= pourThresholdAngle)
        {
            if (rotationSpeed <= maxSafePourSpeed)
            {
                if (barFill != null)
                {
                    barFill.fillAmount += fillRate * Time.deltaTime;
                    barFill.fillAmount = Mathf.Clamp01(barFill.fillAmount);
                }

                if(!isPouring)
                {
                    isPouring = true;
                    AudioManager.Instance.PlaySFX(pourSFX, 0.5f, 1.0f, true);
                    AudioManager.Instance.PlayCorrectSFX();
                }
            }
            else
            {
                if (prepStepManager != null)
                {
                    //If the player pours too fast
                    prepStepManager.DecreaseTimerByPour();
                }

                if (!isPouring)
                {
                    isPouring = true;
                    AudioManager.Instance.PlaySFX(pourSFX, 0.5f, 1.0f, true);
                    AudioManager.Instance.PlayMistakeSFX();
                }
            } //if the object isnt pouring
        } else
        {
            if (isPouring)
            {
                isPouring = false;
                AudioManager.Instance.StopSFX(pourSFX);
            }
        }
    }

    private void HandlePourAnimation(float rotation)
    {
        if (pourAnimator == null) GetAnimComp();

        pourAnimator.SetBool("isPouring", isPouring);
        if (isPouring) pourObject.SetActive(true);
        else pourObject.SetActive(false);

        if (rotation >= maxTiltAngle * 0.3 && rotation < maxTiltAngle * 0.6)
        {
            pourAnimator.SetFloat("tilt", 0.4f);
        }
        if (rotation >= maxTiltAngle*0.6)
        {
            pourAnimator.SetFloat("tilt", 0.7f);
        } else
        {
            pourAnimator.SetFloat("tilt", 0f);
        }
    }

    private void GetAnimComp()
    {
        pourAnimator = pourObject.GetComponent<Animator>();
    }
}