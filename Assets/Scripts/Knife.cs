using UnityEngine;

public class Knife : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
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
