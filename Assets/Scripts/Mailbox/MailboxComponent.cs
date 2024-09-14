using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MailboxComponent : MonoBehaviour
{
    [SerializeField] List<Readable> shipDocs = new List<Readable>();

    [SerializeField] private UnityEvent onMailOpened;

    [SerializeField] private UnityEvent onNoMailLeft;
    public void ReadNextMail()
    {
        if (Readable.isReading)
            return;

        if (shipDocs.Count == 0)
        {
            onNoMailLeft?.Invoke();
            return;
        }

        Readable next = shipDocs[0];
        next.ShowReadable();
        shipDocs.Remove(next);
        onMailOpened?.Invoke();
    }


}
