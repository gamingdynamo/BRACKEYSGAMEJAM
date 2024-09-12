using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickableInteractionComponent : MonoBehaviour
{

    [SerializeField] private PickableObject pickableType;

    [Header("Interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [SerializeField] private UnityEvent delayedEvent;
    [SerializeField] private bool animate = true;
    [SerializeField] private bool destroy = false;

    public void Interact(Transform interactor)
    {
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
            FirstPersonController.Instance.canMove = false;
            yield return null;
            while (currentPickable.IsAnimating())
                yield return null;

            FirstPersonController.Instance.canMove = true;
            delayedEvent?.Invoke();
            if (destroy)
            {
                PickableComponent.pickedObject = null;
                Destroy(currentPickable.gameObject);
            }
        }
    }
}
