using UnityEngine;
using UnityEngine.Events;

public class PickableComponent : MonoBehaviour
{
    public static PickableComponent pickedObject;

    public PickableObject pickableObjectType;
    [SerializeField] private bool animating;

    [SerializeField] private UnityEvent onPickedEvent;

    [SerializeField] private Vector3 pickPositionOffset = new Vector3(0.2f, 0, 1.5f);
    [SerializeField] private Vector3 pickRotationOffset = new Vector3(0, 0, 0);
    public void Animate() => GetComponent<Animator>()?.SetTrigger("Animate");
    public bool IsAnimating()
    {
        Animator animator = GetComponent<Animator>();
        if (!animator) return false;
        bool isTransitioning = animator.IsInTransition(0);
        bool isDefaultLayer = animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Default");
        return isTransitioning || !isDefaultLayer;
    }
    private void Update()
    {
        animating = IsAnimating();
    }

    public void PickUp(Transform interactor)
    {
        if (pickedObject != null)
            return;

        DisableCollider();
        pickedObject = this;
        transform.SetParent(interactor);
        transform.localPosition = pickPositionOffset;
        transform.localEulerAngles = pickRotationOffset;
        onPickedEvent?.Invoke();
    }


    public void DisableCollider()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }

    public void EnableCollider()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = true;
    }
}
