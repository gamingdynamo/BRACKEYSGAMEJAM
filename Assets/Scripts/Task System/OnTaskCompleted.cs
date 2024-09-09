using UnityEngine;
using UnityEngine.Events;

public class OnTaskCompleted : MonoBehaviour
{
    [SerializeField] private Task relatedTask;
    [SerializeField] private UnityEvent onTaskCompletedEvent;

    private void OnEnable() => TaskSGT.Instance.AddListenerOnTaskCompleted(OnTaskCompletedAction);
    private void OnDisable() => TaskSGT.Instance.RemoveListenerOnTaskCompleted(OnTaskCompletedAction);
    public void OnTaskCompletedAction(Task task)
    {
        if (relatedTask != task)
            return;

        onTaskCompletedEvent?.Invoke();
    }
}
