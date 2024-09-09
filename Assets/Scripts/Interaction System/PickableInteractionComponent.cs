using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickableInteractionComponent : MonoBehaviour
{

    [SerializeField] private PickableObject pickableType;
    [SerializeField] private bool enabled = true;

    [Header("Interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [SerializeField] private UnityEvent delayedEvent;
    [SerializeField] private float delay = 1;
    [SerializeField] private bool animate = true;

    public void SetActive(bool active) => enabled = active;

    public void Interact(Transform interactor)
    {
        if (!enabled)
            return;

        PickableComponent currentPickable = PickableComponent.pickedObject;

        if (!currentPickable)
            return;

        if (pickableType != currentPickable.pickableObjectType)
            return;

        interactEvent?.Invoke();

        if (animate)
            currentPickable.Animate();

        StartCoroutine(delayedInteraction());

        IEnumerator delayedInteraction()
        {
            yield return new WaitForSeconds(delay);
            delayedEvent?.Invoke();
        }
    }
}
