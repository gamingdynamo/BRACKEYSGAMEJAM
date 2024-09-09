using UnityEngine;
using UnityEngine.Events;

public class DropSpotComponent : MonoBehaviour
{

    [SerializeField] private PickableObject useSpecificType;
    private PickableComponent heldItem;

    
    [SerializeField] private UnityEvent onPickedEvent;
    [SerializeField] private UnityEvent onDroppedEvent;

    public void DropOrPick(Transform interactor)
    {
        if(heldItem != null)
        {
            heldItem.PickUp(interactor);
            heldItem = null;
            onPickedEvent?.Invoke();
            return;
        }

        PickableComponent currentPickable = PickableComponent.pickedObject;
        if (currentPickable == null)
            return;

        if (useSpecificType && currentPickable.pickableObjectType != useSpecificType)
            return;

        heldItem = currentPickable;
        heldItem.transform.SetParent(null);
        heldItem.transform.position = transform.position;
        heldItem.transform.rotation = transform.rotation;
        PickableComponent.pickedObject = null;
        onDroppedEvent?.Invoke();

    }
}
