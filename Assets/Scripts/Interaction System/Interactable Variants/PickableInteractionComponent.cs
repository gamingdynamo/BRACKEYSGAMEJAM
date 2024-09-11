using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickableInteractionComponent : MonoBehaviour
{

    [SerializeField] private PickableObject pickableType;

    [Header("Interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [SerializeField] private UnityEvent delayedEvent;
    [SerializeField] private float delay = 1;
    [SerializeField] private bool animate = true;



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
            FirstPersonController.Instance.canLook = false;
            FirstPersonController.Instance.canMove = false;
            yield return null;
            while (currentPickable.IsAnimating())
                yield return null;

            // yield return new WaitForSeconds(delay);

            FirstPersonController.Instance.canLook = true;
            FirstPersonController.Instance.canMove = true;
            delayedEvent?.Invoke();
        }
    }
}
