using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KitchenDoor : MonoBehaviour
{
    public Vector3 openRotation = new Vector3(0, -75f, 0);
    private bool isOpen = false;
    private Vector3 closedRotation;

    void Start()
    {
        closedRotation = transform.localEulerAngles;
        XRSimpleInteractable grab = GetComponent<XRSimpleInteractable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ow");
        if (collision.gameObject.CompareTag("KitchenDoorKey"))
        {
            isOpen = !isOpen;
            transform.localEulerAngles = isOpen ? openRotation : closedRotation;
            Destroy(collision.gameObject);
        }
    }
}
