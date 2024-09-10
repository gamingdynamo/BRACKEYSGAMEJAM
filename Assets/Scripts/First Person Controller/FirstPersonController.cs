using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private float maxAngle = 90;

    [SerializeField] private float acceleration = 5;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float sprintSpeed = 12;
    [SerializeField] private bool canSprint = true;

    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;
    private Vector3 lerpedMove;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraRotationX += mouseX * sensitivity;
        cameraRotationY -= mouseY * sensitivity;
        cameraRotationX = cameraRotationX % 360;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -maxAngle, maxAngle);

        float hor_move = Input.GetAxis("Horizontal");
        float ver_move = Input.GetAxis("Vertical");

        Vector3 vertical_direction = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
        Vector3 horizontal_direction = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up);
        Vector3 movement = horizontal_direction * hor_move + vertical_direction * ver_move;
        movement = Vector3.ClampMagnitude(movement * 0.5f, 1);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;
        movement *= isSprinting ? sprintSpeed : moveSpeed;

        lerpedMove = Vector3.Lerp(lerpedMove, movement, 1 - Mathf.Exp(-(Mathf.Abs(acceleration) * Time.deltaTime)));
        controller.SimpleMove(movement);
    }



    private void LateUpdate()
    {
        Vector3 lookVector = new Vector3(cameraRotationY, cameraRotationX, 0);
        cam.transform.localEulerAngles = lookVector;
    }
}
