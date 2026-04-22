using UnityEngine;
public class Dishes : MonoBehaviour
{
    public GameObject firstDish;
    public GameObject secondDish;
    public GameObject thirdDish;
    public GameObject fourthDish;
    public GameObject emptyDish;
    public GameObject filledDish;

    public int counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dish"))
        {
            if(counter == 0)
            {
                firstDish.SetActive(true);
            } else if(counter == 1)
            {
                secondDish.SetActive(true);
            } else if(counter == 2)
            {
                thirdDish.SetActive(true);
            } else if(counter == 3)
            {
                fourthDish.SetActive(true);
            }

            counter++;

            if (counter == 4)
            {
                firstDish.SetActive(false);
                secondDish.SetActive(false);
                thirdDish.SetActive(false);
                fourthDish.SetActive(false);
                emptyDish.SetActive(false);
                filledDish.SetActive(true);

                this.gameObject.GetComponent<ProximityCanvas>().canvas.SetActive(false);
                Destroy(this.gameObject);
            }

            Destroy(other.gameObject);
        }
    }

}