using UnityEngine;
using UnityEngine.UI;

public class ArmorShopButton : MonoBehaviour
{
    public int armorAmount;
    public int price;

    public Button armorButton;

    public ShopManager shopManager;

    public void Buy()
    {
        shopManager.BuyArmor(armorAmount, price);
    }
}
