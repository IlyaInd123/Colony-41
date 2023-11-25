using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] Image healthBar;
    [SerializeField] Transform waypointParent;
    List<Transform> waypoints = new();
    int currentIndex;
    public float currentHealth;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        currentHealth = maxHealth;
        foreach (Transform child in waypointParent)
        {
            if (waypoints.Contains(child)) { continue; }
            waypoints.Add(child);
        }
        waypoints.RemoveAll(child => child == null);
    }

    private void Update()
    {
        if (waypoints.Count > 0)
        {
            Transform waypoint = waypoints[currentIndex];
            Vector3 direction = waypoint.position - transform.position;
            float distance = direction.magnitude;
            if (currentIndex == waypoints.Count - 1 && distance < 0.1f)
            {
                Destroy(gameObject);
            }
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Time.deltaTime * speed);
            if (distance < 0.1f)
            {
                currentIndex = currentIndex < waypoints.Count - 1 ? currentIndex + 1 : waypoints.Count - 1;
            }
        }
    }
}
