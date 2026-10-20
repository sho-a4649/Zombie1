using UnityEngine;

public class HealShopButton : MonoBehaviour
{
    public int healAmount;
    public int price;

    public ShopManager shopmanager;

    public void Buy()
    {
        shopmanager.BuyHeal(healAmount, price);
    }
}
