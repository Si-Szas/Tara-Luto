using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    private void Start()
    {
        playerInput.enabled = true;
    }

    private void OnDestroy()
    {
        playerInput.enabled = false;
    }
}
