using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 1.5f;

    [SerializeField] private GameObject interactable;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            GetInteractable()?.Interact();
    }

    public InteractableComponent GetInteractable()
    {
        bool hit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance, -1, QueryTriggerInteraction.Ignore);
        return hit ? hitInfo.collider.GetComponentInChildren<InteractableComponent>() : null;
    }
}
