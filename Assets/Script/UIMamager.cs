using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMamager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI armorText;
    //public TextMeshProUGUI waveText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerText;

    public Slider hpBar;
    public Slider shieldBar;
    public GameObject hpFill;
    public GameObject shieldFill;

    public PlayerHealth playerHealth;
    public WaveManager waveManager;
    public PlayerMoney playerMoney;

    private void Update()
    {
        hpText.text = "" + Mathf.CeilToInt(playerHealth.currentHealth);

        armorText.text = "" + playerHealth.armor;

        //waveText.text = "Wave : " + waveManager.CurrentPhase;

        moneyText.text = "$ " + playerMoney.money;

        int time = Mathf.CeilToInt(waveManager.GetTimeRemaining());

        if (waveManager.IsBreakTime())
        {
            timerText.text = "BreakTime : " + time;
        }
        else
        {
            timerText.text = "Wave " + waveManager.CurrentPhase + " : " + time;
        }

        if (playerHealth.armor <= 0)
        {
            armorText.color = Color.gray;
        }
        else
        {
            armorText.color = Color.cyan;
        }

        hpBar.maxValue = playerHealth.maxHealth;

        hpBar.value = playerHealth.currentHealth;

        hpFill.SetActive(playerHealth.currentHealth > 0);

        shieldBar.maxValue = playerHealth.maxArmor;

        shieldBar.value = playerHealth.armor;

        shieldFill.SetActive(playerHealth.armor > 0);
    }
}
