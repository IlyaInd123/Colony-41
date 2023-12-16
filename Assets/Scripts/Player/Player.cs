using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawnPoint;
    bool shooting;
    Camera mainCamera;
    GameObject laserInstance;
    LaserWeapon laserWeapon;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        laserWeapon = GetComponentInChildren<LaserWeapon>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Movement();

        transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);

        if (laserWeapon != null && Input.GetKey(KeyCode.Mouse0))
        {
            laserWeapon.Fire(laserSpawnPoint.position, mainCamera.transform.forward);
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        HandleLaser();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        movement = transform.TransformDirection(movement);
        if (movement.magnitude != 0)
        {
            rb.velocity += movement;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, movementSpeed);
        }
        else
        {
            rb.velocity = new(0, rb.velocity.y, 0);
        }
    }

    private void HandleLaser()
    {
        if (shooting && laserInstance == null)
        {
            laserInstance = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation, laserSpawnPoint);
        }
        else if (shooting && laserInstance != null)
        {
            laserInstance.transform.forward = mainCamera.transform.forward;
            laserInstance.transform.position = laserSpawnPoint.position;
        }
        else if (!shooting && laserInstance != null)
        {
            Destroy(laserInstance);
        }
    }
}