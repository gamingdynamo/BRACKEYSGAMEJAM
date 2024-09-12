using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LighthouseController : MonoBehaviour
{
    [SerializeField] private Light Spotlight;

    [Header("Settings")]
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private Vector2 angleRangeY = new Vector2(0, 80);
    [SerializeField] private Vector2 angleRangeX = new Vector2(-90, 90);

    [Header("Settings")]
    private ShipNav controlledShip = null;
    public static UnityAction<ShipNav> onControlChanged;


    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;

    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (!Spotlight.gameObject.activeSelf)
            return; 

        CalculateRotationValues();

        if (Input.GetKeyDown(KeyCode.Tab))
            EnableSpotlight(false);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ship = GetShipInView();
            var foundPosition = TryGetRaycastPosition(out Vector3 position);

            if (ship && ship != controlledShip)
            {
                controlledShip = ship;
                onControlChanged?.Invoke(ship);
                return;
            }

            if (controlledShip && foundPosition)
                controlledShip.SetDirection(position);
        }

    }

    

    public void EnableSpotlight(bool state)
    {
        Spotlight.gameObject.SetActive(state);
        FirstPersonController.Instance.gameObject.SetActive(!state);
    }

    private void LateUpdate()
    {
        Vector3 lookVector = new Vector3(cameraRotationY, cameraRotationX, 0);
        Spotlight.transform.localEulerAngles = lookVector;
    }

    private void CalculateRotationValues()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraRotationX += mouseX * sensitivity;
        cameraRotationY -= mouseY * sensitivity;
        cameraRotationX = cameraRotationX % 360;
        cameraRotationY = Mathf.Clamp(cameraRotationY, angleRangeY.x, angleRangeY.y);
        cameraRotationX = Mathf.Clamp(cameraRotationX, angleRangeX.x, angleRangeX.y);
    }

    private ShipNav GetShipInView()
    {
        bool hit = Physics.Raycast(Spotlight.transform.position, Spotlight.transform.forward, out RaycastHit hitinfo, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        return hit ? hitinfo.transform.GetComponent<ShipNav>() : null;  
    }


    private bool TryGetRaycastPosition(out Vector3 position)
    {
        bool hit = Physics.Raycast(Spotlight.transform.position, Spotlight.transform.forward, out RaycastHit hitinfo, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        position = hit ? hitinfo.point : Vector3.zero;
        return hit;
    }
}
