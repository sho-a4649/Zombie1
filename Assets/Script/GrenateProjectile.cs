using UnityEngine;

public class GrenateProjectile : MonoBehaviour
{
    public float damage;
    public float radius;

    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(effect, 2f);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            ZombieHealth zombie = hit.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
