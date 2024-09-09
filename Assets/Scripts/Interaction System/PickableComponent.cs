using UnityEngine;
using UnityEngine.Events;

public class PickableComponent : MonoBehaviour
{
    public static PickableComponent pickedObject;

    public PickableObject pickableObjectType;
    
    [SerializeField] private UnityEvent animationEvent;
    [SerializeField] private Vector3 pickOffset = new Vector3(0.2f, 0, 1.5f);

    public void Animate() => animationEvent?.Invoke();

    public void PickUp(Transform interactor)
    {
        if (pickedObject != null)
            return;

        DisableCollider();
        pickedObject = this;
        transform.SetParent(interactor);
        transform.localPosition = pickOffset;  
        transform.localRotation = Quaternion.identity;
    }


    public void DisableCollider()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }

    public void EnableCollider()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = true;
    }
}
