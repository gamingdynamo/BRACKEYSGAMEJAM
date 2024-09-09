using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    void Update()
    {
        var interactable = GetInteractable();
        if (interactable && Input.GetKeyDown(KeyCode.F))
            interactable.Interact(transform);
    }

    public InteractableComponent GetInteractable()
    {
        bool hit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance, -1, QueryTriggerInteraction.Collide);
        return hit ? hitInfo.collider.GetComponentInChildren<InteractableComponent>() : null;
    }
}
