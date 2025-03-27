using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public string enemyTag = "Enemy"; // Ensure all enemies have this tag

    public GameObject ball;
    public float offset_y = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) return; // No enemies to shoot at

        // Spawn the bullet at the player's position
        GameObject bullet = GameObject.Instantiate(ball, transform.position, Quaternion.identity);

        bullet.AddComponent<Bullet>();

        // Get Rigidbody and set the bullet's velocity to move towards the enemy
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (closestEnemy.position - transform.position).normalized; // Direction towards the enemy
        rb.velocity = direction * bulletSpeed;
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}