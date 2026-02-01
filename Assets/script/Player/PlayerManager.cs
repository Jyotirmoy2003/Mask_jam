using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("References")]
    [SerializeField] private RagdollSwitcher ragdollSwitcher;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject maskObject;

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

        if(playerInput)
        playerInput.enabled = false;
        // Switch to ragdoll
        if (ragdollSwitcher != null)
        {
            ragdollSwitcher.EnableRagdoll();
        }

        // Disable player control scripts here if needed
        // Example:
        // GetComponent<PlayerController>().enabled = false;

        Debug.Log("Player died and ragdoll activated");
        GameManager.Instance.OnPlayerDie();
    }

    public void ListenToMaskModeChange(Component sender,object data)
    {
        if((bool)data)
        {
            maskObject.SetActive(true);
        }
        else
        {
            maskObject.SetActive(false);
        }
    }
}
