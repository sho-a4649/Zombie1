using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("HP : " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
