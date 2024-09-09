using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent<Transform> interactionEvent;
    public void Interact(Transform interactor) => interactionEvent?.Invoke(interactor);
}
