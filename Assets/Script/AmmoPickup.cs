using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoType ammoType;

    public int amount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerAmmo playerAmmo = other.GetComponent<PlayerAmmo>();

        if (playerAmmo == null)
            return;

        switch(ammoType)
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

        Destroy(gameObject);
    }
}
