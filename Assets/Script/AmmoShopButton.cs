using UnityEngine;

public class AmmoShopButton : MonoBehaviour
{
    public AmmoType ammoType;

    public int price;

    public int amount;

    public ShopManager shopManager;

    public void Buy()
    {
        shopManager.BuyAmmo(ammoType, price, amount);
    }
}
