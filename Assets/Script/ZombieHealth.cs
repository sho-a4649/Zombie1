using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float health = 100f;

    public int reward = 50;

    public GameObject moneyPickup;
    public GameObject smallAmmoPickup;
    public GameObject mediumAmmoPickup;
    public GameObject shellPickup;
    public GameObject explosivePickup;

    [HideInInspector]
    public WaveManager waveManager;

    /*private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
    }*/

    public void TakeDamage(float damage)
    {
        health -= damage;

        /*int drop = Random.Range(0, 100);

        if (drop < 70)
        {
            Instantiate(moneyPickup, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ammoPickup, transform.position, Quaternion.identity);
        }*/

        if (health <= 0)
        {
            //waveManager.ZombieKilled();
            DropLoot();

            GameStats.Instance.zombiesKilled++;

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

    void DropLoot()
    {
        int roll = Random.Range(0, 100);

        GameObject drop = null;

        if (roll < 50)
        {
            drop = moneyPickup;
        }
        else if (roll < 70)
        {
            drop = smallAmmoPickup;
        }
        else if (roll < 90)
        {
            drop = mediumAmmoPickup;
        }
        else if (roll < 97)
        {
            drop = shellPickup;
        }
        else
        {
            drop = explosivePickup;
        }

        if (drop != null)
        {
            GameObject pickup = Instantiate(drop, transform.position + Vector3.up * 0.25f, Quaternion.identity);

            Destroy(pickup, 20f);
        }
    }
}
