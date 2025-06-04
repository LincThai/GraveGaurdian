using UnityEngine;

public class Enemy : MonoBehaviour
{
    // set variables
    public int maxHealth = 100;
    int currentHealth;
    // the animator if there are animations


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // deal the damage
        currentHealth -= damage;

        // if there is an animation hurt animation play

        // check if they die
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        // if there is play a death animation

        // disable or destroy the enemy
        Destroy(gameObject);
    }
}
