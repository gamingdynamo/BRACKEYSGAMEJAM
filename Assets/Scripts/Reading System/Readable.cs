using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readable : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public void ShowReadable()
    {
        if (canvas.activeSelf)
            return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.SetActive(true);
        FirstPersonController.Instance.canMove = false;
    }

    public void HideReadable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas.SetActive(false);
        FirstPersonController.Instance.canMove = true;

    }
}
