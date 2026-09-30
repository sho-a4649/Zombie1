using System.Collections;
using TMPro;
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
    public float maxSpreadAngle = 5f; // ¸“x

    public LineRenderer bulletTracer; // ’e“¹
    public ParticleSystem muzzleFlash; // ƒ}ƒYƒ‹ƒtƒ‰ƒbƒVƒ…
    public TextMeshProUGUI ammoText; // Žc’e”UI

    private float nextFireTime;

    private bool isReloading;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (isReloading)
            return;

        crosshair.isAiming = Input.GetMouseButton(1);

        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            //crosshair.ShootEffect();
            crosshair.AddShootSpread(30f);
            Shoot();
            currentAmmo--;

            nextFireTime = Time.time + fireRate;

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

        ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        Debug.Log("muzzleflash.Play");

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

        float spreadPercent = crosshair.currentSpread / crosshair.MaxPossibleSpread;

        float currentSpreadAngle = spreadPercent * maxSpreadAngle;

        shootDirection = Quaternion.Euler(Random.Range(-currentSpreadAngle, currentSpreadAngle),
            Random.Range(-currentSpreadAngle, currentSpreadAngle), 0) * shootDirection;

        Vector3 hitPoint;
        Vector3 tracerEndPoint;

        if (Physics.Raycast(firePoint.position, shootDirection, out RaycastHit hit, 100f))
        {
            hitPoint = hit.point;
            tracerEndPoint = hit.point;

            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
        else
        {
            hitPoint = firePoint.position + shootDirection * 100f;
            tracerEndPoint = firePoint.position + shootDirection * 100f;
        }

        StartCoroutine(ShowTracer(firePoint.position, hitPoint));
        StartCoroutine(ShowTracer(firePoint.position, tracerEndPoint));

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

    IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        LineRenderer tracer = Instantiate(bulletTracer);

        tracer.enabled = true;

        tracer.SetPosition(0, start);
        tracer.SetPosition(1, end);

        yield return new WaitForSeconds(0.05f);

        tracer.enabled = false;
    }

    IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;

        ammoText.color = Color.white;
    }
}
