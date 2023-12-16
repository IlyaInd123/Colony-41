using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeTrap : Trap
{
    [SerializeField] float tickRate = 0.5f;
    [SerializeField] float damagePerSecond = 5f;
    List<GameObject> keysToUpdate = new();
    Dictionary<GameObject, (IDamageable damagable, float timer)> damageableDictionary = new();

    private void Update()
    {
        if (active)
        {
            ApplyDamage(damageableDictionary);
        }
    }

    void ApplyDamage(Dictionary<GameObject, (IDamageable damagable, float timer)> damageableDictionary)
    {
        keysToUpdate = new(damageableDictionary.Keys);
        foreach (GameObject target in keysToUpdate)
        {
            (IDamageable damagable, float timer) = damageableDictionary[target];
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (target != null)
                {
                    damagable.TakeDamage(damagePerSecond * tickRate);
                }
                else
                {
                    damageableDictionary.Remove(target);
                    continue;
                }
                timer = tickRate;
            }
            damageableDictionary[target] = (damagable, timer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && !damageableDictionary.ContainsKey(other.gameObject))
        {
            damageableDictionary.Add(other.gameObject, (enemy, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (damageableDictionary.ContainsKey(other.gameObject) && other.TryGetComponent(out IDamageable _))
        {
            damageableDictionary.Remove(other.gameObject);
        }
    }
}