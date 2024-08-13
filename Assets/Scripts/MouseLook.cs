using UnityEngine;
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public float maxClampAngle;

    [SerializeField] private float inputLagPeriod;

    public Transform playerBody;
    float xRotation = 0f;

    private Vector2 lastInputEvent;
    private float inputLagTime;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseControl();
    }

    private Vector2 GetMouseInput()
    {
        inputLagTime += Time.deltaTime;

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if ((Mathf.Approximately(0, mouseInput.x) && Mathf.Approximately(0, mouseInput.y)) == false || inputLagTime >= inputLagPeriod)
        {
            lastInputEvent = mouseInput;
            inputLagTime = 0f;
        }

        return lastInputEvent;
    }

    public void MouseControl()
    {
        Vector2 mouseMovement = GetMouseInput() * mouseSensitivity;

        xRotation -= mouseMovement.y;
        xRotation = Mathf.Clamp(xRotation, -maxClampAngle, maxClampAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseMovement.x);
    }
}
