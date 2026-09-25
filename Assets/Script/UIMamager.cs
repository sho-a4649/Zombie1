using TMPro;
using UnityEngine;

public class UIMamager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI moneyText;

    public PlayerHealth playerHealth;
    public WaveManager waveManager;
    public PlayerMoney playerMoney;

    private void Update()
    {
        hpText.text = "HP : " + Mathf.CeilToInt(playerHealth.currentHealth);

        waveText.text = "Wave : " + waveManager.CurrentPhase;

        moneyText.text = "$ " + playerMoney.money;
    }
}
