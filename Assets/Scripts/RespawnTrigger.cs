using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    // respawn point empty game object transform
    public Transform respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // deactivate player and character controller components
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<PlayerController>().enabled = false;
            // move player to that position
            other.gameObject.transform.position = respawnPosition.position;
            // deactivate player and character controller components
            other.GetComponent<CharacterController>().enabled = true;
            other.GetComponent<PlayerController>().enabled = true;
        }
    }
}
