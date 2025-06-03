using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // set variables
    // if I animate an attack this is the animator

    // primary variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 30;

    // Update is called once per frame
    void Update()
    {
        // get the player input
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        // if I animate an attack animation play animation 

        // detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // check if their is an attack point
        if (attackPoint == null) { return; }
        // draw a sphere to show it
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
