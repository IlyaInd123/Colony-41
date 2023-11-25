using UnityEngine;

internal interface IDamageable
{
    GameObject gameObject { get; }

    void TakeDamage(float damage);
}