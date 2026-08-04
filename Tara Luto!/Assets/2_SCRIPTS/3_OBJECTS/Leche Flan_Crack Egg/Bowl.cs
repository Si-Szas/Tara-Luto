using UnityEngine;

public class Bowl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("col detected");
        if (collision.gameObject.tag == "eggYolk")
        {
            Destroy(collision.gameObject);
        }
    }
}
