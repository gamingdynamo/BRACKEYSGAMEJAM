using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent interactionEvent;
    public void Interact() => interactionEvent?.Invoke();
}
