using TMPro;
using UnityEngine;

public class UIMamager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemyText;

    public PlayerHealth playerHealth;
    public WaveManager waveManager;

    private void Update()
    {
        hpText.text = "HP : " + Mathf.CeilToInt(playerHealth.currentHealth);

        waveText.text = "Wave : " + waveManager.CurrentPhase;

        enemyText.text = "Enemy : " + waveManager.ZombiesAlive;
    }
}
