using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (currentHealth < 20)
        {
            GetComponent<EnemyAI>().StartFleeing();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}