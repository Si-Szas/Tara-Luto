using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        //Make sure that the touch input exists
        if (Pointer.current == null) return;

        //Detect click / press
        if (Pointer.current.press.wasPressedThisFrame)
        {
            Debug.Log($"Touched at: {Pointer.current.position.ReadValue()}");
        }

        //Detect hold
        if (Pointer.current.press.isPressed)
        {
            Debug.Log("Currently holding");
        }

        //Detect release
        if (Pointer.current.press.wasReleasedThisFrame)
        {
            Debug.Log("Released");
        }
    }
}
