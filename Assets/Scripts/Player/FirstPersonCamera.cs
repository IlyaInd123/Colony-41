using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 1f;
    [SerializeField][Range(50f, 90f)] float maxLookAngle = 75f;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        xRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        yRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation = Mathf.Clamp(yRotation, -maxLookAngle, maxLookAngle);
        Vector3 rotation = new(yRotation, xRotation, 0f);
        transform.rotation = Quaternion.Euler(rotation);
        if (transform.parent != null)
        {
            transform.parent.forward = Vector3.ProjectOnPlane(transform.forward, new Vector3(0, 1));
        }
    }
}