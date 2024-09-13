using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class ShipNav : MonoBehaviour
{
    [SerializeField] private float moveSpeed =1f;
    [SerializeField] private float rotationSpeed = 1f;
    public UnityEvent collisionEvent;
    public UnityEvent savedEvent;

    [SerializeField] private UnityEvent onShipSelected;
    [SerializeField] private UnityEvent onShipDeselected;

    public static List<ShipNav> allShips = new List<ShipNav>();
    public static List<ShipNav> shipsCrashed = new List<ShipNav>();
    public static List<ShipNav> shipsSaved = new List<ShipNav>();

    private bool isSelected;
    private Vector3 direction;

    private void Awake() => direction = transform.forward;
    private void OnEnable() => LighthouseController.onControlChanged += OnSelect;
    private void OnDisable() => LighthouseController.onControlChanged -= OnSelect;

    private void Start() => allShips.Add(this);
    void Update()
    {
        var lookRotation = Quaternion.LookRotation(direction);
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            1 - Mathf.Exp(-Mathf.Abs(rotationSpeed) * Time.deltaTime));

    }

    public void SetDirection(Vector3 worldPosition)
    {
        var cachedDirection = worldPosition - transform.position;
        cachedDirection = Vector3.ProjectOnPlane(cachedDirection, Vector3.up);
        cachedDirection.Normalize();

        direction = cachedDirection;
    }

    private void OnSelect(ShipNav ship)
    {
        if (ship == this)
        {
            onShipSelected?.Invoke();
            isSelected = true;
        }
        else if (isSelected)
        {
            onShipDeselected?.Invoke();
            isSelected = false;
        }
    }



}
