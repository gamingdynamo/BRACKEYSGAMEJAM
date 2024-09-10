using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartEvent;
    private void Start() => onStartEvent?.Invoke();
}