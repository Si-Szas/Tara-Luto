using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableSpawner : MonoBehaviour
{
    [Header("Prefab Object")]
    [SerializeField] private GameObject draggablePrefab; //Object to spawn

    private Camera mainCamera;
    private Collider2D myCollider;

    void Start()
    {
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Pointer.current == null) return;

        //Tap on spawner
        if (Pointer.current.press.wasPressedThisFrame)
        {
            //Get the position of tap
            Vector2 tapPosition = Pointer.current.position.ReadValue();

            //2D position of tap
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, 0));

            //idk this is to detect where the mouse is and spawn it basically
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider == myCollider)
            {
                SpawnDraggableItem(worldPosition);
            }
        }
    }

    private void SpawnDraggableItem(Vector3 spawnPosition)
    {
        //Spawn game object and set active
        GameObject spawnedObject = Instantiate(draggablePrefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);

        //Debug shi
        Debug.Log("EGG IS BORN");
    }
}