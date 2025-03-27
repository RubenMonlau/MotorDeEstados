using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider boxCollider = other.GetComponent<BoxCollider>();

            // Only proceed if this is the BoxCollider (not SphereCollider)
            if (boxCollider != null)
            {
                Debug.Log("PlayerDetected");
                PlayerHealth player = other.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Debug.Log($"Player hit! Health left: {player.currentHealth}");
                    Destroy(gameObject);
                }
            }
        }
    }
}