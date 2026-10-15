using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int value = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMoney money = other.GetComponent<PlayerMoney>();

            if (money != null)
            {
                money.AddMoney(value);
            }

            Destroy(gameObject);
        }
    }
}
