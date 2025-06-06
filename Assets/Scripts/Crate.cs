using UnityEngine;

public class Crate : Enemy
{
    // set variables
    public int healthHealed = 30;

    PlayerCombat playerCombat;

    public override void Start()
    {
        playerCombat = PlayerManager.instance.player.GetComponent<PlayerCombat>();
        // uses original code to setup health
        base.Start();
    }

    public override void Death()
    {
        // heal the player
        playerCombat.currentPlayerHealth += healthHealed;

        // use the original code
        base.Death();
    }
}
