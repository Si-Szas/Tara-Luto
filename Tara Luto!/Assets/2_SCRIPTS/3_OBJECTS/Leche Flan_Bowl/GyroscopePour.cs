using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GyroscopePour : MonoBehaviour
{
    [SerializeField] Image progressBar;
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            if (UnityEngine.InputSystem.Gyroscope.current != null)
            {
                InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Gyroscope.current != null && UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            Vector3 rotationRate = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();

            transform.Rotate(new Vector3(0f, 0f, rotationRate.z) * Time.deltaTime);
        }

            Debug.Log("Rotation: " +  transform.eulerAngles.z);
        if (transform.eulerAngles.z > 100 && transform.eulerAngles.z < 150)
        {
            Debug.Log("rotation right");
            progressBar.fillAmount += 0.0002f;
        }
    }
}
