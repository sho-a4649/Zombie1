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

    public int price;

    public bool isShotgun;
    public int pelletCount = 8;

    public bool isRocketLauncher;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float explosionRadius;
    public float explosionDamage;

    public AudioClip shootSound;
}
