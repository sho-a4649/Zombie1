using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public float damage;
    public float radius;

    private void OnCollisionEnter(Collision collision)
    {
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
