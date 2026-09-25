using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;
    public CrosshairController crosshair;

    public float range = 100f; // ŽË’ö‹——£
    public float damage = 25f; // —^ƒ_ƒ[ƒW—Ê
    public Transform firePoint; // eŒû

    public int maxAmmo = 30; // ƒƒ“ƒ}ƒK
    public int currentAmmo;

    public float reloadTime = 2f; // ƒŠƒ[ƒhŽžŠÔ
    public float fireRate = 0.1f; // ˜AŽË‘¬“x(1=1•b1”­, 0.1=1•b10”­)
    private float nextFireTime;

    private bool isReloading;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            //crosshair.ShootEffect();
            crosshair.AddShootSpread(30f);
            Shoot();
            currentAmmo--;

            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Ray centerRay = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Vector3 targetPoint;

        if (Physics.Raycast(centerRay, out RaycastHit centerHit, 100f))
        {
            targetPoint = centerHit.point;
        }
        else
        {
            targetPoint = centerRay.origin + centerRay.direction * 100f;
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        if (Physics.Raycast(firePoint.position, shootDirection, out RaycastHit hit, 100f))
        {
            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }

        Debug.DrawRay(firePoint.position, shootDirection * 100f, Color.red, 1f);

        /*if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit : " + hit.collider.name);

            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }*/
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}
