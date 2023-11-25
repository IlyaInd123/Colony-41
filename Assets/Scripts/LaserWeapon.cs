using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] float damagePerSecond = 1f;

    public void ShootLaser()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, range))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}