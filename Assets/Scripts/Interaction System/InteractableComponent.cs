using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    public string InteractableName = "Interactable";

    [SerializeField] private UnityEvent<Transform> interactionEvent;
    [SerializeField] private bool interactionEnabled = true;

    public bool IsInteractionEnabled => interactionEnabled;
    public void SetName(string name) => InteractableName = name;
    public void EnableInteraction(bool state) => interactionEnabled = state;
    public void Interact(Transform interactor)
    {
        if (interactionEnabled)
            interactionEvent?.Invoke(interactor);
    }
}
