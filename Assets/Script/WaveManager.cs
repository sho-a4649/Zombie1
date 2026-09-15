using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject zombiePrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private int currentPhase = 0; // フェーズ数

    private float timer; // 残り時間

    private bool isBreakTime = false; // フェーズ状態

    public int zombiesAlive = 0;

    public int CurrentPhase => currentPhase;
    public int ZombiesAlive => zombiesAlive;

    private int[] phaseTimes = // 各フェーズ時間(秒)
    {
        30,
        60,
        90,
        120,
        180
    };

    private int[] maxZombieCounts = // 各フェーズ最大ゾンビ数
    {
        10,
        20,
        35,
        50,
        80
    };

    private int currentMaxZombies;

    private void Start()
    {
        StartPhase();

        // 毎秒スポーン状況確認
        StartCoroutine(SpawnCheckRoutine());
    }

    private void Update()
    {
        if (isBreakTime)
            return;

        timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StartCoroutine(StartBreak());
            }
    }

    void StartPhase()
    {
        currentPhase++; // 次のフェーズ

        int index = Mathf.Min(currentPhase - 1, phaseTimes.Length - 1);

        timer = phaseTimes[index]; // 制限時間設定

        currentMaxZombies = maxZombieCounts[index]; // 最大ゾンビ数設定

        isBreakTime = false; // フェーズ開始

        Debug.Log("Phase " + currentPhase + " Start");
    }

    IEnumerator SpawnCheckRoutine()
    {
        while (true)
        {
            if (!isBreakTime)
            {
                MaintainZombieCount();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    /*IEnumerator StartBreak()
    {
        isBreakTime = true; // フェーズ終了

        Debug.Log("Break Time");

        ZombieHealth[] zombies = FindObjectsByType<ZombieHealth>(FindObjectsSortMode.None);

        foreach (ZombieHealth zombie in zombies) // 全ゾンビ削除
        {
            Destroy(zombie.gameObject);
        }

        yield return new WaitForSeconds(30f); // 30秒休憩

        StartPhase(); // フェーズ開始
    }*/

    void MaintainZombieCount() // ゾンビ数を一定に保つ
    {
        //ZombieHealth[] zombies = FindObjectsByType<ZombieHealth>(FindObjectsSortMode.None);

        while (zombiesAlive < currentMaxZombies) // 少なければスポーン
        {
            SpawnZombie();

            //zombies = FindObjectsByType<ZombieHealth>(FindObjectsSortMode.None);
        }
    }

    void SpawnZombie() // ゾンビスポーン
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)]; // ランダムなスポーン地点

        GameObject zombie = Instantiate(zombiePrefab, spawn.position, Quaternion.identity);

        ZombieHealth zombieHealth = zombie.GetComponent<ZombieHealth>();

        if (zombieHealth != null)
        {
            zombieHealth.waveManager = this;
        }

        zombiesAlive++;

        //Instantiate(zombiePrefab, spawn.position, Quaternion.identity); // ゾンビ生成
    }

    IEnumerator StartBreak()
    {
        isBreakTime = true;

        Debug.Log("Break Time");

        ZombieHealth[] zombies = FindObjectsByType<ZombieHealth>(FindObjectsSortMode.None);

        foreach (ZombieHealth zombie in zombies)
        {
            Destroy(zombie.gameObject);
        }

        zombiesAlive = 0;

        yield return new WaitForSeconds(30f);

        StartPhase();
    }

    public float GetTimeRemaining() // 残り時間
    {
        return timer;
    }

    public bool IsBreakTime() // フェーズ状態
    {
        return isBreakTime;
    }

}
