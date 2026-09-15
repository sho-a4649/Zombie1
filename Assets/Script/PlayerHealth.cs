using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100; // Å‘åHP
    public float currentHealth; // Œ»Ý‚ÌHP

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) // ƒ_ƒ[ƒW‚ðŽó‚¯‚é
    {
        currentHealth -= damage;

        if (currentHealth >= 0)
        {
            Debug.Log("HP : " + currentHealth);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over");
        }
    }
}
