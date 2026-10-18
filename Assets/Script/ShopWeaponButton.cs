using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponButton : MonoBehaviour
{
    public int weaponIndex;

    public ShopManager shopManager;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void Buy()
    {
        shopManager.BuyWeapon(weaponIndex);

        button.interactable = false;
    }
}
