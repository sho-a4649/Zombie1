using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float health = 100f;

    public WaveManager waveManager;

    private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            waveManager.ZombieKilled();

            Destroy(gameObject);
        }
    }
}
