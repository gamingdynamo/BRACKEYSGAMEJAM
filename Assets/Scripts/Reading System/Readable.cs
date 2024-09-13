using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Readable : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private UnityEvent onRead;

    public static bool isReading = false;

    private bool wasRead = false;
    public void ShowReadable()
    {
        if (isReading)
            return;

        if (canvas.activeSelf)
            return;

        isReading = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.SetActive(true);
        FirstPersonController.Instance.canMove = false;

        if (!wasRead)
        {
            onRead?.Invoke();
            wasRead = true;
        }
    }

    public void HideReadable()
    {

        isReading = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas.SetActive(false);
        FirstPersonController.Instance.canMove = true;

    }
}
