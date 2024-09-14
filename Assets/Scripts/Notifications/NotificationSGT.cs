using UnityEngine;
using UnityEngine.Events;

public class NotificationSGT : GenericSingleton<NotificationSGT>
{
    [Header("Data Components")]
    [SerializeField] private GameObject _notificationPreset;
    [SerializeField] private GameObject _contents;
    [Header("Settings")]
    [SerializeField] private float _notificationDisplayTime;
    [SerializeField] private float _notificationBlendTime;
    [SerializeField] private UnityEvent onNotificationAdded;


    public void AddNotification(string message)
    {
        if (message == string.Empty || !_contents || !_notificationPreset)
            return;

        GameObject notification = Instantiate(_notificationPreset);
        notification.SetActive(true);
        notification.transform.SetParent(_contents.transform);
        NotificationComponent notifyComponent = notification.GetComponent<NotificationComponent>();

        if (!notifyComponent) {
            Destroy(notification);
            return;
        }
        notifyComponent.StartNotification(message, _notificationDisplayTime, _notificationBlendTime);
        onNotificationAdded?.Invoke();

    }
}



