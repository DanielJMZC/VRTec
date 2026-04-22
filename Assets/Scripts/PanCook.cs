using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

public class PanCook : MonoBehaviour
{
    public Transform stackPoint;

    bool cooking;

    public GameObject prefab;
    

        void OnTriggerStay(Collider other)
        {
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();

            if (grab != null && !grab.isSelected)
            {
                if(other.gameObject.name.Contains("Meat_Raw")) {
                    if (!cooking)
                    {
                        cooking = true;
                        StartCoroutine(SnapToStack(other.gameObject));
                    }
                }
            }
        }

      IEnumerator SnapToStack(GameObject ingredient)
        {
            
            BoxCollider col = ingredient.GetComponent<BoxCollider>();

            float colliderThickness = col.bounds.size.y * ingredient.transform.localScale.y;;

            float distanceToBottom = ingredient.transform.position.y - col.bounds.min.y;

            float spawnY = stackPoint.position.y + distanceToBottom;

            ingredient.transform.SetParent(this.transform);
            ingredient.transform.position = new Vector3(stackPoint.position.x, spawnY, stackPoint.position.z);
            ingredient.transform.rotation = stackPoint.rotation;

            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.detectCollisions = false; 
            }

            ingredient.GetComponent<XRGrabInteractable>().enabled = false;
            
            GameController.Instance.sFXManager.PlayMeatSFX();
            yield return new WaitForSeconds(5);

            Destroy(ingredient);
            Instantiate(prefab, stackPoint.position, stackPoint.rotation);
            cooking = false;
        }
}
