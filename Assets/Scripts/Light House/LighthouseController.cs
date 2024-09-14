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
    [SerializeField] private float setDestinationTime = 2f;



    [Header("Events")]
    [SerializeField] private UnityEvent onShipSelected;
    [SerializeField] private UnityEvent onShipSetDestination;
    [SerializeField] private UnityEvent onLighthouseEntered;
    [SerializeField] private UnityEvent onLighthouseExited;

    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;


     private float setDestinationTimer = 0f;

    private ShipNav controlledShip = null;
    public static UnityAction<ShipNav> onControlChanged;

    void Update()
    {
        if (!Spotlight.gameObject.activeSelf)
            return; 

        CalculateRotationValues();

        if (Input.GetKeyDown(KeyCode.Tab))
            EnableSpotlight(false);

        var ship = GetShipInView();

        if (ship && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ship != controlledShip)
            {
                controlledShip = ship;
                onControlChanged?.Invoke(ship);
                return;
            }
        }

        bool isSettingDestination = Input.GetKey(KeyCode.Mouse0) && ship == null && controlledShip != null;
        setDestinationTimer = isSettingDestination ?
            Mathf.Min(setDestinationTimer + Time.deltaTime, setDestinationTime +1) :
            Mathf.Max(setDestinationTimer - Time.deltaTime, 0);

        bool canSetDestination = setDestinationTimer >= setDestinationTime;
        if (canSetDestination && controlledShip != null)
        {
            bool foundPosition = TryGetRaycastPosition(out Vector3 position);
            if (foundPosition)
            {
                controlledShip.SetDirection(position);
                setDestinationTimer = 0;
                onShipSetDestination?.Invoke();
                return;
            }
        }
    }



    public void EnableSpotlight(bool state)
    {
        if (!state && !FirstPersonController.Instance.controlledByLighthouse)
            return;

        Spotlight.gameObject.SetActive(state);
        Tween(state);
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

    private void Tween(bool state)
    {
        if (state)
        {
            StopAllCoroutines();
            StartCoroutine(Tween(FirstPersonController.Instance.GetCameraTransform(), Spotlight.transform));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Tween(FirstPersonController.Instance.GetCameraTransform(), FirstPersonController.Instance.transform));
        }


        IEnumerator Tween(Transform from, Transform to)
        {
            FirstPersonController.Instance.CanZoom = state;

            if (state)
            {
                FirstPersonController.Instance.controlledByLighthouse = state;
                onLighthouseEntered?.Invoke();
            }

            float timer = 0;
            from.SetParent(to);

            Vector3 startPos = from.position;
            Quaternion startRot = from.rotation;
            while (timer < 1)
            {
                float t = timer / 1.0f;
                from.position = Vector3.Lerp(startPos, to.position, t);
                from.rotation = Quaternion.Lerp(startRot, to.rotation, t);
                timer += Time.deltaTime;
                yield return null;
            }

            from.localPosition = Vector3.zero;
            from.localEulerAngles = Vector3.zero;
            from.localScale = Vector3.one;

            if(!state)
            {
                FirstPersonController.Instance.controlledByLighthouse = state;
                FirstPersonController.Instance.ResetCameraOffset();
                onLighthouseExited?.Invoke();
            }
        }
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
