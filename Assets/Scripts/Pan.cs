using UnityEngine;

public class Pan : MonoBehaviour
{
    // This function is called automatically when a collision starts
    void OnCollisionEnter(Collision collision)
    {
        // 1. Check if the object hit has a specific tag
        if (collision.gameObject.CompareTag("Food"))
        {
            Meat meat = collision.gameObject.GetComponent<Meat>();

             if (meat != null && meat.preparedPrefab != null)
            {
                Instantiate(meat.preparedPrefab, 
                            collision.transform.position, 
                            collision.transform.rotation);
            }


            Destroy(collision.gameObject);
        }

    }
}
