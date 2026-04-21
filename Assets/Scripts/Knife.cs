using UnityEngine;

public class Knife : MonoBehaviour
{
    // This function is called automatically when a collision starts
    void OnCollisionEnter(Collision collision)
    {
        // 1. Check if the object hit has a specific tag
        if (collision.gameObject.CompareTag("Food"))
        {
            Food food = collision.gameObject.GetComponent<Food>();

             if (food != null && food.preparedPrefab != null)
            {
                Instantiate(food.preparedPrefab, 
                            collision.transform.position, 
                            collision.transform.rotation);
            }


            Destroy(collision.gameObject);
        }

    }
}
