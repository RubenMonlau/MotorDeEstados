using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Tag to identify the enemies

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            // Apply damage or effect to the enemy
            Destroy(collision.gameObject); // Destroy enemy (optional, adjust as needed)
            Destroy(gameObject); // Destroy bullet after collision
        }
    }
}