using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;
    public CrosshairController crosshair;
    public WeaponData currentWeapon;
    public PlayerAmmo playerAmmo;

    //public float range = 100f; // 射程距離
    //public float damage = 25f; // 与ダメージ量
    public Transform firePoint; // 銃口

    //public int maxAmmo = 30; // ワンマガ
    public int currentAmmo;

    //public float reloadTime = 2f; // リロード時間
    //public float fireRate = 0.1f; // 連射速度(1=1秒1発, 0.1=1秒10発)
    //public float maxSpreadAngle = 5f; // 精度

    //public Animator animator;

    public LineRenderer bulletTracer; // 弾道
    public ParticleSystem muzzleFlash; // マズルフラッシュ
    public TextMeshProUGUI ammoText; // 残弾数UI

    public GameObject crossHair;

    private float nextFireTime;

    private bool isReloading;

    private AudioSource audioSource;

    private void Start()
    {
        //currentAmmo = currentWeapon.maxAmmo;
        crossHair.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentWeapon == null)
            return;

        if (ShopManager.IsShopOpen)
            return;
        /*if (currentWeapon == null)
            return;*/

        if (isReloading)
            return;

        bool isAiming = Input.GetMouseButton(1);

        crosshair.isAiming = isAiming;
        //animator.SetBool("IsAiming", isAiming);

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < currentWeapon.maxAmmo && GetReserveAmmo() > 0)
        {
            StartCoroutine(Reload());
        }

        if (currentAmmo <= 0 && GetReserveAmmo() > 0)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            //crosshair.ShootEffect();
            crosshair.AddShootSpread(currentWeapon.shootSpread);
            Shoot();
            currentAmmo--;

            nextFireTime = Time.time + currentWeapon.fireRate;

            if (currentAmmo <= 0)
            {
                ammoText.color = Color.red;
                StartCoroutine(Reload());
            }
            else if (currentAmmo <= 5 && currentAmmo >= 1)
            {
                ammoText.color = Color.yellow;
            }
        }

        ammoText.text = currentWeapon.weaponName + "\n" +  currentAmmo + " / " + GetReserveAmmo();
    }

    void Shoot()
    {
        muzzleFlash.Play();

        if (currentWeapon.shootSound != null)
        {
            audioSource.PlayOneShot(currentWeapon.shootSound);
        }
        
        GameStats.Instance.shotsFired++;

        if (currentWeapon.isRocketLauncher)
        {
            FireRocket();
        }
        else if (currentWeapon.isGrenadeLauncher)
        {
            FireGrenade();
        }
        else if (currentWeapon.isShotgun)
        {
            FireShotgun();
        }
        else
        {
            FireBullet();
        }
    }

    IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        LineRenderer tracer = Instantiate(bulletTracer);

        tracer.SetPosition(0, start);
        tracer.SetPosition(1, end);

        yield return new WaitForSeconds(0.05f);

        Destroy(tracer.gameObject);
    }

    IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;

        //Debug.Log("Reloading...");

        yield return new WaitForSeconds(currentWeapon.reloadTime);

        //currentAmmo = currentWeapon.maxAmmo;
        int reserveAmmo = GetReserveAmmo();

        int neededAmmo = currentWeapon.maxAmmo - currentAmmo;

        int reloadAmount = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += reloadAmount;

        RemoveReserveAmmo(reloadAmount);

        isReloading = false;

        ammoText.color = Color.white;
    }

    public void SetWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;

        currentAmmo = weapon.maxAmmo;

        crosshair.normalSpread = weapon.hipSpread;

        crosshair.aimSpread = weapon.aimSpread;

        crossHair.SetActive(true);
    }

    int GetReserveAmmo()
    {
        if (currentWeapon == null)
            return 0;

        switch(currentWeapon.ammoType)
        {
            case AmmoType.Small:
                return playerAmmo.smallAmmo;

            case AmmoType.Medium:
                return playerAmmo.mediumAmmo;

            case AmmoType.Shell:
                return playerAmmo.shellAmmo;

            case AmmoType.Explosive:
                return playerAmmo.explosiveAmmo;
        }

        return 0;
    }

    void RemoveReserveAmmo(int amount)
    {
        switch (currentWeapon.ammoType)
        {
            case AmmoType.Small:
                playerAmmo.smallAmmo -= amount;
                break;

            case AmmoType.Medium:
                playerAmmo.mediumAmmo -= amount;
                break;

            case AmmoType.Shell:
                playerAmmo.shellAmmo -= amount;
                break;

                case AmmoType.Explosive:
                playerAmmo.explosiveAmmo -= amount;
                break;
        }
    }

    void FireBullet()
    {
        float randomX = Random.Range(-crosshair.AccuracySpread, crosshair.AccuracySpread);

        float randomY = Random.Range(-crosshair.AccuracySpread, crosshair.AccuracySpread);

        Ray centerRay = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2 + randomX, Screen.height / 2 + randomY));

        Vector3 targetPoint;

        if (Physics.Raycast(centerRay, out RaycastHit centerHit, currentWeapon.range))
        {
            targetPoint = centerHit.point;
        }
        else
        {
            targetPoint = centerRay.origin + centerRay.direction * currentWeapon.range;
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        Vector3 tracerEndPoint;

        if (Physics.Raycast(firePoint.position, shootDirection, out RaycastHit hit, currentWeapon.range))
        {
            tracerEndPoint = hit.point;

            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                GameStats.Instance.shotsHit++;

                zombie.TakeDamage(currentWeapon.damage);
            }
        }
        else
        {
            tracerEndPoint = firePoint.position + shootDirection * currentWeapon.range;
        }

        StartCoroutine(ShowTracer(firePoint.position, tracerEndPoint));

        Debug.DrawRay(firePoint.position, shootDirection * currentWeapon.range, Color.red, 1f);
    }

    void FireShotgun()
    {
        bool hitSomething = false;

        Vector3 tracerEndPoint;

        for (int i = 0; i < currentWeapon.pelletCount; i++)
        {
            float randomX = Random.Range(-crosshair.AccuracySpread * 1f, crosshair.AccuracySpread * 1f);

            float randomY = Random.Range(-crosshair.AccuracySpread * 1f, crosshair.AccuracySpread * 1f);

            Ray centerRay = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2 + randomX, Screen.height / 2 + randomY));

            Vector3 targetPoint;

            if (Physics.Raycast(centerRay, out RaycastHit centerHit, currentWeapon.range))
            {
                targetPoint = centerHit.point;
            }
            else
            {
                targetPoint = centerRay.origin + centerRay.direction * currentWeapon.range;
            }

            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

            if (Physics.Raycast(firePoint.position, shootDirection, out RaycastHit hit, currentWeapon.range))
            {
                //Debug.Log("Hit Collider : " + hit.collider.name);

                tracerEndPoint = hit.point;

                ZombieHealth zombie = hit.collider.GetComponentInParent<ZombieHealth>();

                if (zombie != null)
                {
                    hitSomething = true;

                    zombie.TakeDamage(currentWeapon.damage);
                }
            }
            else
            {
                tracerEndPoint = firePoint.position + shootDirection * currentWeapon.range;
            }

            StartCoroutine(ShowTracer(firePoint.position,tracerEndPoint));
        }

        if (hitSomething)
        {
            GameStats.Instance.shotsHit++;
        }
    }

    void FireRocket()
    {
        Debug.Log("Rocket Fire");

        GameObject rocket = Instantiate(currentWeapon.projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(-90f, 0f, 0f));

        Rigidbody rb = rocket.GetComponent<Rigidbody>();

        Debug.Log(rb);

        rb.linearVelocity = firePoint.forward * currentWeapon.projectileSpeed;

        RocketProjectile rocketScript = rocket.GetComponent<RocketProjectile>();

        rocketScript.damage = currentWeapon.explosionDamage;

        rocketScript.radius = currentWeapon.explosionRadius;

        //rocketScript.explosionEffect = currentWeapon.explosionEffect;
    }

    void FireGrenade()
    {
        GameObject grenade = Instantiate(currentWeapon.projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.linearVelocity = firePoint.forward * currentWeapon.projectileSpeed;
    }
}
