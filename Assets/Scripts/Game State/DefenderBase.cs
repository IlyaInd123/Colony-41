using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenderBase : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float damageOnCollision = 10f;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image healthBar;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = $"Base HP: {currentHealth}";
    }

    private void Update()
    {
        if (healthText != null)
        {
            healthText.text = $"Base HP: {currentHealth}";
        }
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
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

    public void ReduceHealth(float percentage)
    {
        currentHealth *= percentage;
    }
}