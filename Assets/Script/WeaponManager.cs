using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;

    public PlayerShoot playerShoot;

    int currentWeapon;

    private void Start()
    {
        SelectWeapon(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectWeapon(3);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectWeapon(4);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectWeapon(5);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            SelectWeapon(6);
    }

    void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        currentWeapon = index;

        WeaponObject weapon = weapons[index].GetComponent<WeaponObject>();

        playerShoot.SetWeapon(weapon.weaponData);
    }
}
