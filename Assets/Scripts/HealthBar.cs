using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // set variable
    public Slider healthBar;

    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void SetHealth(int health)
    {
       healthBar.value = health;
    }
}
