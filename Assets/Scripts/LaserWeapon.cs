using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] float damagePerSecond = 1f;
    [SerializeField] LayerMask layersToIgnore;
    public float Range { get => range; }

    public void Fire(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out var hit, Range, ~layersToIgnore))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}