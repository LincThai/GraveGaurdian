using UnityEngine;

public class Enemy : MonoBehaviour
{
    // set variables
    public int maxHealth = 100;
    int currentHealth;
    // the animator
    public Animator enemyAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        // set the current health equal to the maximum
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // deal the damage
        currentHealth -= damage;

        // hurt animation play
        enemyAnimator.SetTrigger("Hurt");

        // play hurt sound
        FindObjectOfType<AudioManager>().Play("EnemyHurt");

        // check if they die
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        // play a death animation
        enemyAnimator.SetBool("IsDead", true);

        // play a death sound
        FindObjectOfType<AudioManager>().Play("Death");

        // disable or destroy the enemy
        Destroy(gameObject);
    }
}
