using UnityEngine;

public class DefenderBase : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float damageOnCollision = 10f;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            currentHealth -= damageOnCollision;
            Destroy(other.gameObject);
            if (currentHealth <= 0)
            {
                Time.timeScale = 0;
                GameManager.Instance.GameOver();
            }
        }
    }
}