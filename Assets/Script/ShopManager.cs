using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public PlayerMoney playerMoney;
    public WeaponManager weaponManager;
    public PlayerAmmo playerAmmo;
    public PlayerHealth playerHealth;

    public static bool IsShopOpen;

    public GameObject shopUI;

    public Button sgButton;
    public Button smgButton;
    public Button arButton;
    public Button lmgButton;
    public Button glButton;
    public Button rpgButton;

    public Button smallammoButton;
    public Button mediumammoButton;
    public Button shellammoButton;
    public Button explosiveammoButton;

    public Button hpButton;
    public Button armorButton;

    private void Update()
    {
        UpdateButtons();
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);

        IsShopOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);

        IsShopOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BuyWeapon(int weaponIndex)
    {
        WeaponObject weapon = weaponManager.weapons[weaponIndex].GetComponent<WeaponObject>();

        int price = weapon.weaponData.price;

        if (playerMoney.money < price)
            return;

        playerMoney.money -= price;

        weaponManager.UnlockWeapon(weaponIndex);
    }

    public void BuyAmmo(AmmoType ammoType, int price, int amount)
    {
        if (playerMoney.money < price)
            return;

        playerMoney.money -= price;

        switch (ammoType)
        {
            case AmmoType.Small:
                playerAmmo.smallAmmo += amount;
                break;

            case AmmoType.Medium:
                playerAmmo.mediumAmmo += amount;
                break;

            case AmmoType.Shell:
                playerAmmo.shellAmmo += amount;
                break;

            case AmmoType.Explosive:
                playerAmmo.explosiveAmmo += amount;
                break;
        }
    }

    public void BuyHeal(int amount, int price)
    {
        // HPMax‚È‚ç”ƒ‚¦‚È‚¢
        if (playerHealth.currentHealth >= playerHealth.maxHealth)
            return;

        if (playerMoney.money < price)
            return;

        playerMoney.money -= price;

        playerHealth.Heal(amount);
    }

    public void BuyArmor(int amount, int price)
    {
        // ArmorMax‚È‚ç”ƒ‚¦‚È‚¢
        if (playerHealth.armor >= playerHealth.maxArmor)
            return;

        if (playerMoney.money < price)
            return;

        playerMoney.money -= price;

        playerHealth.AddArmor(amount);
    }

    void UpdateButtons()
    {
        sgButton.interactable = playerMoney.money >= 300 && !weaponManager.weapons[1].GetComponent<WeaponObject>().purchased;
        smgButton.interactable = playerMoney.money >= 1000 && !weaponManager.weapons[2].GetComponent<WeaponObject>().purchased;
        arButton.interactable = playerMoney.money >= 2000 && !weaponManager.weapons[3].GetComponent<WeaponObject>().purchased;
        lmgButton.interactable = playerMoney.money >= 3500 && !weaponManager.weapons[4].GetComponent<WeaponObject>().purchased;
        glButton.interactable = playerMoney.money >= 5000 && !weaponManager.weapons[5].GetComponent<WeaponObject>().purchased;
        rpgButton.interactable = playerMoney.money >= 5000 && !weaponManager.weapons[6].GetComponent<WeaponObject>().purchased;

        smallammoButton.interactable = playerMoney.money >= 150;
        mediumammoButton.interactable = playerMoney.money >= 150;
        shellammoButton.interactable = playerMoney.money >= 150;
        explosiveammoButton.interactable = playerMoney.money >= 150;

        hpButton.interactable = playerMoney.money >= 300 && playerHealth.currentHealth < playerHealth.maxHealth;
        armorButton.interactable = playerMoney.money >= 300 && playerHealth.armor < playerHealth.maxArmor;
    }
}
