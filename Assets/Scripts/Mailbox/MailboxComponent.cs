using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxComponent : MonoBehaviour
{
    [SerializeField] List<Readable> shipDocs = new List<Readable>();

    public void ReadNextMail()
    {
        if (Readable.isReading)
            return;

        if (shipDocs.Count == 0)
            return;

        Readable next = shipDocs[0];
        next.ShowReadable();
        shipDocs.Remove(next);
    }


}
