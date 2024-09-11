using TMPro;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;
    [SerializeField] private TextMeshProUGUI textMesh;

    void Update()
    {     
        var interactable = GetInteractable();

        DisplayText(interactable);

        if (interactable && Input.GetKeyDown(interactionKey))
            interactable.Interact(transform);
    }

    private InteractableComponent GetInteractable()
    {
        bool hit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance, -1, QueryTriggerInteraction.Collide);
        return hit ? hitInfo.collider.GetComponentInChildren<InteractableComponent>() : null;
    }

    private void DisplayText(InteractableComponent interactable)
    {
        if (textMesh == null)
            return;

        textMesh.text = interactable && interactable.InteractionEnabled ? $"{interactable.InteractableName} ({interactionKey.ToString()})" : string.Empty;

    }
}
