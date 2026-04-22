using UnityEngine;
public class Pan : MonoBehaviour
{
    public GameObject firstPan;
    public GameObject secondPan;
    public int counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan")) 
        {
            if(other.gameObject.name.Contains("First"))
            {
                firstPan.SetActive(true);
                counter++;
            } else
            {
                secondPan.SetActive(true);
                counter++;
            }

            if (counter == 2)
            {
                this.gameObject.GetComponent<ProximityCanvas>().canvas.SetActive(false);
                Destroy(this.gameObject);
            }

            Destroy(other.gameObject);
        }
    }

}