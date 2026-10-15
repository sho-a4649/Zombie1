using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public float damage;
    public float fireRate;
    public int maxAmmo;
    public float reloadTime;

    public float aimSpread;
    public float hipSpread;
    public float shootSpread;

    public float range;

    public AmmoType ammoType;

    public AudioClip shootSound;
}
