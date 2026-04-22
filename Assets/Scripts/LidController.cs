using UnityEngine;

public class LidController : MonoBehaviour
{
   public Transform spawnpoint;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ow");
        if (collision.gameObject.CompareTag("Hammer"))
        {
            Debug.Log("HAMMER");
            spawnpoint.GetComponent<IngredientStation>().enabled = true;
            Destroy(gameObject);
        }
    }
}
