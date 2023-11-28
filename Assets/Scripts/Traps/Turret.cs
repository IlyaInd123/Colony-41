using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LaserWeapon))]
public class Turret : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float fireAngleThreshold = 5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawnPoint;
    GameObject laserInstance;
    WaveSystem waveSystem;
    Transform target;
    LaserWeapon laserWeapon;
    bool shooting = false;
    
    private void Start()
    {
        laserWeapon = GetComponent<LaserWeapon>();
        waveSystem = FindObjectOfType<WaveSystem>();
    }

    private void OnValidate()
    {
        if (laserWeapon == null)
        {
            laserWeapon = GetComponent<LaserWeapon>();
        }
    }

    private void Update()
    {
        target = GetClosestEnemy();
        if (target != null && Vector3.Distance(transform.position, target.position) <= laserWeapon.Range)
        {
            Vector3 directionToTarget = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Vector3.Angle(transform.forward, directionToTarget) < fireAngleThreshold && Vector3.Distance(transform.position, target.position) <= laserWeapon.Range)
            {
                laserWeapon.Fire(laserSpawnPoint.position, directionToTarget);
                shooting = true;
            }
        }
        else
        {
            shooting = false;
        }

        HandleLaser();
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
            laserInstance.transform.LookAt(target);
            laserInstance.transform.position = laserSpawnPoint.position;
        }
        else if (!shooting && laserInstance != null)
        {
            Destroy(laserInstance);
        }
    }

    Transform GetClosestEnemy()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= laserWeapon.Range) { return target; }
        Transform closestEnemy = null;
        if (waveSystem == null || waveSystem.GetEnemies().Count == 0) { return closestEnemy;}
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in waveSystem.GetEnemies())
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    private void OnDrawGizmos()
    {
        if (target != null && Vector3.Angle(transform.forward, target.position - transform.position) < fireAngleThreshold)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }

#if UNITY_EDITOR
        if (laserWeapon != null)
        {
            Handles.color = new Color(0, 1, 0, 0.1f);
            Handles.DrawSolidDisc(transform.position, Vector3.up, laserWeapon.Range);
        }
#endif
    }
}