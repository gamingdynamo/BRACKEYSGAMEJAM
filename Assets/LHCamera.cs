using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private Vector3 destPoint;
    public GameObject selector;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        bool hit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hitInfo, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        if (!hit)
            return;

        destPoint = hitInfo.point;
        Debug.DrawRay(destPoint, Vector3.up * 5, Color.red, 2);
        selector.transform.position = destPoint;
    }
}
