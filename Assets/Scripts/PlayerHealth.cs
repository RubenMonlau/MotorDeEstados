using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateColor();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateColor()
    {
        if (currentHealth > 70)
            GetComponent<Renderer>().material.color = Color.magenta;
        else if (currentHealth > 40)
            GetComponent<Renderer>().material.color = Color.yellow;
        else
            GetComponent<Renderer>().material.color = Color.red;
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Add logic for player death (e.g., reload scene, game over screen)
    }
}
