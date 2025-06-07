using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // set variables
    // enemy Movement/AI variables
    public float lookRadius = 10f;

    // references
    Transform target;
    NavMeshAgent agent;
    public Animator enemyAnimator;

    // enemy attack variables
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 1f;
    float nextAttackTime = 0;

    public Transform attackPoint;
    public LayerMask PlayerLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // setting references
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // measure the distance from the target
        float distance = Vector3.Distance(target.position, transform.position);

        // check it in comparison to the look radius
        if (distance <= lookRadius)
        {
            // set the destination as the target's position to follow
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                // face the target
                FaceTarget();

                if (Time.time >= nextAttackTime)
                {
                    // attack the target
                    AttackPlayer();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void AttackPlayer()
    {
        // play an attack animation
        enemyAnimator.SetTrigger("Attack");

        // play slash/attack sound
        FindObjectOfType<AudioManager>().Play("Slash");

        // detect everything that is hit
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, PlayerLayers);

        // deal damage
        foreach (Collider player in hitPlayers)
        {
            player.GetComponent<PlayerCombat>()?.PlayerTakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // set gizmo color
        Gizmos.color = Color.red;
        // draw sphere to show area where enemy will react to the player
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        // check if their is an attack point
        if (attackPoint == null) { return; }
        // draw a sphere to show attack area/range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
