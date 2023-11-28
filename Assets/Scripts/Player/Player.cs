using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawnPoint;
    GameObject laserInstance;
    float currentHealth;
    LaserWeapon laserWeapon;
    Rigidbody rb;
    private bool shooting;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        laserWeapon = GetComponentInChildren<LaserWeapon>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        Movement();

        if (laserWeapon != null && Input.GetKey(KeyCode.Mouse0))
        {
            laserWeapon.Fire(transform.forward);
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void HandleLaser()
    {
        if (shooting && laserInstance == null)
        {
            laserInstance = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
            laserInstance.transform.parent = laserSpawnPoint;
        }
        else if (shooting && laserInstance != null)
        {
            laserInstance.transform.forward = transform.forward;
            laserInstance.transform.position = laserSpawnPoint.position;
        }
        else if (!shooting && laserInstance != null)
        {
            Destroy(laserInstance);
        }
    }
}