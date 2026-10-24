using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public float damage;
    public float radius;

    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Rocket Hit");

        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(effect, 2f);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            ZombieHealth zombie = hit.GetComponentInParent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
