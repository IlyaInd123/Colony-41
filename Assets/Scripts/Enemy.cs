using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable, ISlowable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] Image healthBar;
    [SerializeField] public Transform waypointParent;
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

    void Start()
    {
        currentHealth = maxHealth;
        if (waypointParent == null) { return; }
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
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Time.deltaTime * speed);
            if (distance < 0.1f)
            {
                currentIndex = currentIndex < waypoints.Count - 1 ? currentIndex + 1 : waypoints.Count - 1;
            }
        }
    }

    public void SetWayPointParent(Transform waypointParent)
    {
        this.waypointParent = waypointParent;
        waypoints.Clear();
        foreach (Transform child in waypointParent)
        {
            if (waypoints.Contains(child)) { continue; }
            waypoints.Add(child);
        }
        waypoints.RemoveAll(child => child == null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform waypoint in waypoints)
        {
            Gizmos.DrawWireSphere(waypoint.position, 0.5f);
        }
    }

    public void ApplySlow(float slowPercentage)
    {
        speed *= 1 - slowPercentage;
    }

    public void RemoveSlow(float slowPercentage)
    {
        speed /= 1 - slowPercentage;
    }
}
