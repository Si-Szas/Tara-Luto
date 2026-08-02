using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PourableObject : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PrepStepManager prepStepManager;

    [Header("Tilt")]
    [SerializeField] private float maxTiltAngle = 60f;
    [SerializeField] private float tiltSensitivity = 5f;
    [SerializeField] private float pourThresholdAngle = 15f;

    [Header("Pour Speed")]
    [SerializeField] private float maxSafePourSpeed = 20f;
    [SerializeField] private float fillRate = 0.15f;

    [Header("Tracker")]
    [SerializeField] private Image barFill;

    [Header("Audio")]
    [SerializeField] private AudioClip pourSFX;

    private float currentRotation = 0f;
    private float lastFrameRotation = 0f;

    void OnEnable()
    {
        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    void OnDisable()
    {
        if (Accelerometer.current != null)
            InputSystem.DisableDevice(Accelerometer.current);
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

        //calculae speed of pour
        float angleDelta = Mathf.Abs(currentRotation - lastFrameRotation);
        float currentRotationSpeed = angleDelta / Time.deltaTime;

        lastFrameRotation = currentRotation;

        //Fill up progress bar
        HandleProgressivePour(currentRotationSpeed);
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
                    AudioManager.Instance.PlaySFX(pourSFX, 0.5f);
                }
            }
            else
            {
                if (prepStepManager != null)
                {
                    prepStepManager.DecreaseTimerByPour();
                }
            }
        }
    }
}