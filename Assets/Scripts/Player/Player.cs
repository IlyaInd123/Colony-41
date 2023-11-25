using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    LaserWeapon laserWeapon;
    Rigidbody rb;

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
            laserWeapon.ShootLaser();
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        movement = transform.TransformDirection(movement);
        rb.MovePosition(transform.position + movementSpeed * Time.deltaTime * movement);
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
}