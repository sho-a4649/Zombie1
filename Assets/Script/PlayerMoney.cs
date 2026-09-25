using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int money = 0;

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }

        return false;
    }
}
