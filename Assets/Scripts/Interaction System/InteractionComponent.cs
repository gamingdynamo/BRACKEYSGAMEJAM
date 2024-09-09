using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            GetInteractable()?.Interact(transform);
    }

    public InteractableComponent GetInteractable()
    {
        bool hit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance, -1, QueryTriggerInteraction.Collide);
        return hit ? hitInfo.collider.GetComponentInChildren<InteractableComponent>() : null;
    }
}
