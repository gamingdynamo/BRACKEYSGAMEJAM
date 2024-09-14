using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : GenericSingleton<FirstPersonController>
{
    [SerializeField] private Camera cam;
    [SerializeField] private Animator camAnimator;

    [SerializeField] private float sensitivity = 2;
    [SerializeField] private float maxAngle = 90;

    [SerializeField] private float acceleration = 5;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float sprintSpeed = 12;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float gravityForce = 5;

    [SerializeField] private float FieldOfViewNormal = 70f;
    [SerializeField] private float FieldOfViewZoomed = 15f;
    [HideInInspector] public bool controlledByLighthouse = false;

    public bool canSprint = true;
    public bool canMove = true;
    [SerializeField] private bool canZoom = false;

    public bool CanZoom 
    { 
        get { return canZoom; } 
        set 
        {
            canZoom = value;

            if (!value)
                EnableZoom(false);
        } 
    }


    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;
    private float hor_move = 0f;
    private float ver_move = 0f;
    private bool isSprinting = false;

    private Vector3 lerpedMove;

    private CharacterController controller;

    private Vector3 initialCameraOffset;

    Vector3 gravity;

    public Transform GetCameraTransform() => cam.transform;
    public Camera GetCamera() => cam;

    public void ResetCameraOffset() => cam.transform.localPosition = initialCameraOffset;
    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        initialCameraOffset = cam.transform.localPosition;
        cam.fieldOfView = FieldOfViewNormal;
    }
    // Update is called once per frame
    void Update()
    {
        if (controlledByLighthouse)
        {
            controller.Move(Vector3.zero);
            HandleZoom();
            return;
        }

        UpdateAnimator();
        CalculateRotationValues();
        CalculateMovementValues();
        CalculateGravity();
        controller.Move(lerpedMove + gravity);
    }


    private void LateUpdate()
    {
        if (controlledByLighthouse)
            return;

        Vector3 lookVector = new Vector3(cameraRotationY, cameraRotationX, 0);
        cam.transform.localEulerAngles = lookVector;
    }


    private void UpdateAnimator()
    {
        Vector2 input = new Vector2(hor_move, ver_move);
        camAnimator.SetBool("Walk", input.magnitude > 0);
        camAnimator.SetBool("Run", isSprinting);
    }
    private void HandleZoom()
    {
        if (!canZoom)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse1))
            EnableZoom(true);
        if (Input.GetKeyUp(KeyCode.Mouse1))
            EnableZoom(false);

    }
    private void EnableZoom(bool state)
    {
        StopAllCoroutines();
        StartCoroutine(EnableZoom());

        IEnumerator EnableZoom()
        {
            float timer = 0;
            float startFOV = cam.fieldOfView;
            float endFOV = state ? FieldOfViewZoomed : FieldOfViewNormal;
            float trans = 0.5f;

            while (timer < trans)
            {
                float t = timer / trans;
                cam.fieldOfView = Mathf.Lerp(startFOV, endFOV, t);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void CalculateGravity()
    {
        var gravityVector = (Vector3.down * gravityForce * Time.deltaTime);
        gravity = controller.isGrounded ? gravityVector : gravity + gravityVector * Time.deltaTime;

        bool jump = Input.GetKeyDown(KeyCode.Space) && canMove && controller.isGrounded;
        if (jump)
            gravity.y = jumpHeight * Time.deltaTime;
    }

    private void CalculateMovementValues()
    {
        hor_move = Input.GetAxis("Horizontal");
        ver_move = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;

        Vector3 vertical_direction = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
        Vector3 horizontal_direction = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up);
        Vector3 movement = horizontal_direction * hor_move + vertical_direction * ver_move;
        movement = Vector3.ClampMagnitude(movement * 0.5f, 1);
        movement *= isSprinting ? sprintSpeed : moveSpeed;
        movement *= Time.deltaTime;
        lerpedMove = Vector3.Lerp(lerpedMove, movement, 1 - Mathf.Exp(-(Mathf.Abs(acceleration) * Time.deltaTime)));
        lerpedMove = canMove ? lerpedMove : Vector3.zero;
    }
    private void CalculateRotationValues()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
       
        cameraRotationX += mouseX * (canMove? sensitivity: 0);
        cameraRotationY -= mouseY * (canMove ? sensitivity : 0);
        cameraRotationX = cameraRotationX % 360;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -maxAngle, maxAngle);
    }
}
