using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public PlayerMoney playerMoney;
    public WeaponManager weaponManager;
    public PlayerAmmo playerAmmo;

    public static bool IsShopOpen;

    public GameObject shopUI;

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
}
