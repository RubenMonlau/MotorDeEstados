using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        // Check if it's an enemy AND the collider is a BoxCollider
        if (other.CompareTag("Enemy"))
        {
            BoxCollider boxCollider = other.GetComponent<BoxCollider>();
            
            // Only proceed if this is the BoxCollider (not SphereCollider)
            if (boxCollider != null && other == boxCollider)
            {
                EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Destroy(gameObject); // Destroy the bullet
                }
            }
            // (Otherwise, ignore SphereCollider triggers)
        }
    }
}