using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100; // Å‘åHP
    public float currentHealth; // Œ»Ý‚ÌHP

    public int armor;
    public int maxArmor = 100;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) // ƒ_ƒ[ƒW‚ðŽó‚¯‚é
    {
        if (armor > 0)
        {
            int armorDamage = Mathf.RoundToInt(damage);

            armor -= armorDamage;

            if (armor < 0)
            {
                currentHealth += armor;
                armor = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        /*if (currentHealth >= 0)
        {
            Debug.Log("HP : " + currentHealth);
        }*/

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;

            FindFirstObjectByType<GameOverManager>().ShowGameOver();
            Debug.Log("Game Over");
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void AddArmor(int amount)
    {
        armor += amount;

        if (armor > maxArmor)
        {
            armor = maxArmor;
        }
    }
}
