using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonStateEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, ISelectHandler
{
    [SerializeField] private UnityEvent onPointerDown;
    [SerializeField] private UnityEvent onPointerEnter;
    [SerializeField] private UnityEvent onPointerExit;
    [SerializeField] private UnityEvent onSelect;

    private bool initialized;
    private void Start() => initialized = true;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (initialized)
            onPointerDown?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (initialized)
            onPointerEnter?.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (initialized)
            onPointerExit?.Invoke();
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (initialized)
            onSelect?.Invoke();
    }
}
