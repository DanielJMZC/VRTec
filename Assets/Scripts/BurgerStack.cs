using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections.Generic;


public class BurgerStack : MonoBehaviour
{
    public float stackHeight = 0;
    public Transform stackPoint;
    
    public List<string> ingredientsInBurger = new List<string>();

    void OnTriggerEnter(Collider other)
    {
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        
        if (grab != null && !grab.isSelected)
        {
            SnapToStack(other.gameObject);
        }
    }

        void SnapToStack(GameObject ingredient)
        {
            
            BoxCollider col = ingredient.GetComponent<BoxCollider>();

            float colliderThickness = col.bounds.size.y; //* ingredient.transform.localScale.y;;

            float distanceToBottom = col.bounds.extents.y + (ingredient.transform.position.y - col.bounds.center.y);

            float spawnY = stackPoint.position.y + stackHeight + distanceToBottom;

            ingredient.transform.SetParent(this.transform);
            ingredient.transform.position = new Vector3(stackPoint.position.x, spawnY, stackPoint.position.z);
            ingredient.transform.rotation = stackPoint.rotation;
          

            stackHeight += colliderThickness;

            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.detectCollisions = false; 
            }

            ingredient.GetComponent<XRGrabInteractable>().enabled = false;
            ingredientsInBurger.Add(ingredient.name);
        }
}