using System.Collections.Generic;
using UnityEngine;

public class SlowingTrap : MonoBehaviour
{
    [SerializeField] float slowPercentage = 0.5f;
    List<ISlowable> slowables = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ISlowable slowable))
        {
            slowables.Add(slowable);
            slowable.ApplySlow(slowPercentage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ISlowable slowable) && slowables.Contains(slowable))
        {
            slowable.RemoveSlow(slowPercentage);
            slowables.Remove(slowable);
        }
    }
}