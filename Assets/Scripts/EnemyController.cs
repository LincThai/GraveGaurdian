using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // set variables
    public float lookRadius = 10f;
    // references
    Transform target;
    NavMeshAgent agent;

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
                // attack the target

                // face the target
                FaceTarget();
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
