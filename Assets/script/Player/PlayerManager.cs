using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("References")]
    [SerializeField] private RagdollSwitcher ragdollSwitcher;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (ragdollSwitcher == null)
            ragdollSwitcher = GetComponent<RagdollSwitcher>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Switch to ragdoll
        if (ragdollSwitcher != null)
        {
            ragdollSwitcher.EnableRagdoll();
        }

        // Disable player control scripts here if needed
        // Example:
        // GetComponent<PlayerController>().enabled = false;

        Debug.Log("Player died and ragdoll activated");
    }
}
