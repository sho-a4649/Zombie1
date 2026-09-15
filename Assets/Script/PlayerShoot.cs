using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;

    public float range = 100f; // Ë’ö‹——£
    public float damage = 25f; // —^ƒ_ƒ[ƒW—Ê

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit : " + hit.collider.name);

            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
