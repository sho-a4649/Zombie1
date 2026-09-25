using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float health = 100f;

    public int reward = 50;

    [HideInInspector]
    public WaveManager waveManager;

    /*private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
    }*/

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //waveManager.ZombieKilled();

            PlayerMoney money = FindFirstObjectByType<PlayerMoney>();

            if (money != null)
            {
                money.AddMoney(reward);
            }

            Die();
        }
    }

    void Die()
    {
        if (waveManager != null)
        {
            waveManager.zombiesAlive--;
        }

        Destroy(gameObject); // ƒ]ƒ“ƒríœ
    }
}
