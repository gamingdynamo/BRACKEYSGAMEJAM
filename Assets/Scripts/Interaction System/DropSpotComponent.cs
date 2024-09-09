using UnityEngine;

public class DropSpotComponent : MonoBehaviour
{

    [SerializeField] private PickableObject useSpecificType;
    [SerializeField] private PickableComponent heldItem;

    public void DropOrPick(Transform interactor)
    {
        if(heldItem != null)
        {
            heldItem.PickUp(interactor);
            heldItem = null;
            return;
        }

        PickableComponent currentPickable = PickableComponent.pickedObject;
        if (currentPickable == null)
            return;

        if (useSpecificType && currentPickable.pickableObjectType != useSpecificType)
            return;

        heldItem = currentPickable;
        currentPickable.transform.SetParent(null);
        currentPickable.transform.position = transform.position;
        currentPickable.transform.rotation = transform.rotation;
        currentPickable.EnableCollider();
        PickableComponent.pickedObject = null;
    }
}
