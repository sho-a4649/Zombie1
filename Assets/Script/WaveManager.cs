using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public int currentWave = 1;

    private int zombiesAlive;

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (zombiesAlive <= 0)
        {
            currentWave++;

            StartWave();
        }
    }

    void StartWave()
    {
        int zombieCount = currentWave * 5;

        zombiesAlive = zombieCount;

        for (int i = 0; i < zombieCount; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject zombie = Instantiate(zombiePrefab, spawn.position, Quaternion.identity);

            zombie.GetComponent<ZombieHealth>().waveManager = this;
        }

        Debug.Log("Wave " + currentWave);
    }

    public void ZombieKilled()
    {
        zombiesAlive--;
    }
}
