using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;

    public float attackRange = 2f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    float nextAttackTime;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();

            if (Time.time >= nextAttackTime)
            {
                PlayerHealth health = player.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }

                nextAttackTime = Time.time + attackCooldown;
            }
        }

        /*if (player != null)
        {
            agent.SetDestination(player.position);
        }*/
    }
}
