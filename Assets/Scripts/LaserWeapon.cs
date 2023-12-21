using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] float damagePerSecond = 1f;
    [SerializeField] LayerMask layersToIgnore;
    public float Range { get => range; }

    public void Fire(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Range, ~layersToIgnore))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    public void IncreaseDamage(float percentage)
    {
        float damageIncrease = percentage * damagePerSecond;
        damagePerSecond += damageIncrease;
    }
}