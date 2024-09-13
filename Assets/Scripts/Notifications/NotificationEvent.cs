using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event System", menuName = "Audio/Notification Event")]
public class NotificationEvent : ScriptableObject
{
    public void AddNotification(string message) => NotificationSGT.Instance.AddNotification(message);
}