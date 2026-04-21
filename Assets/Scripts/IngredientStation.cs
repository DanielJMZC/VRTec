using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class IngredientStation : MonoBehaviour
{
    public GameObject ingredientPrefab;
    private XRSimpleInteractable interactable;

    void OnEnable()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(OnGrabStation);
    }

    void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnGrabStation);
    }

    void OnGrabStation(SelectEnterEventArgs args)
    {
        GameObject ingredient = Instantiate(ingredientPrefab, 
        args.interactorObject.transform.position,
        args.interactorObject.transform.rotation);

        XRGrabInteractable grab = ingredient
        .GetComponent<XRGrabInteractable>();
        
        if (grab != null)
            args.manager.SelectEnter(args.interactorObject, grab);
    }
}