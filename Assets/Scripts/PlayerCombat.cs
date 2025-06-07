using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // set variables
    // Animator
    public Animator playerAnimator;

    // primary variables
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 30;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxPlayerHealth = 200;
    public int currentPlayerHealth;
    public HealthBar healthBar;

    void Start()
    {
        // set health
        currentPlayerHealth = maxPlayerHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // get the player input
            if (Input.GetKey(KeyCode.Mouse0))
            {
                // anything in range (in front of player)
                Attack();
                // increases the time till the next attack based on the rate
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (currentPlayerHealth > maxPlayerHealth)
        {
            // ensures player health does not go above the maximum
            currentPlayerHealth = maxPlayerHealth;
        }

        healthBar.SetHealth(currentPlayerHealth);
    }

    public void Attack()
    {
        // trigger an attack parameter in the animator to play the attack animation
        playerAnimator.SetTrigger("Attack");

        // play slash/attack sound
        FindObjectOfType<AudioManager>().Play("Slash");

        // detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
            enemy.GetComponent<Crate>()?.TakeDamage(attackDamage);
        }
    }

    public void PlayerTakeDamage (int damage)
    {
        // deal the damage
        currentPlayerHealth -= damage;

        // play the hurt sound for the player
        FindObjectOfType<AudioManager>().Play("PlayerHurt");

        // check if player dies(Loses)
        if (currentPlayerHealth <= 0)
        {
            Debug.Log("You Lose");
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
