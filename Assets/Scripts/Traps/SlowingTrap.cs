using System.Collections.Generic;
using UnityEngine;

public class SlowingTrap : Trap
{
    [SerializeField] float slowPercentage = 0.5f;
    List<ISlowable> slowables = new();

    private void Update()
    {
        if (active)
        {
            foreach (ISlowable slowable in slowables)
            {
                if (slowable != null && !slowable.IsSlowed)
                {
                    slowable.ApplySlow(slowPercentage);
                }
                else if (slowable == null)
                {
                    slowables.Remove(slowable);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ISlowable slowable))
        {
            slowables.Add(slowable);
            if (active)
            {
                slowable.ApplySlow(slowPercentage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ISlowable slowable) && slowables.Contains(slowable))
        {
            if (slowable.IsSlowed)
            {
                slowable.RemoveSlow(slowPercentage);
            }
            slowables.Remove(slowable);
        }
    }
}