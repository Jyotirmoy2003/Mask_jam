using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] GameEvent Event_OnplayerDied;
    private int currentHealth;

    [Header("References")]
    [SerializeField] private RagdollSwitcher ragdollSwitcher;
    [SerializeField] PlayerInput playerInput;


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
        Event_OnplayerDied?.Raise(this,true);
    }

    public void ListenToMaskModeChange(Component sender,object data)
    {
        if((bool)data)
        {
            MaskOn();
        }
        else
        {
            MaskOff();
        }
    }
    #region Mask

    [Header("Mask Pivot")]
    [SerializeField] private Transform maskPivot;

    [Header("Rotation")]
    [SerializeField] private Vector3 maskOnRotation = Vector3.zero;        // on face
    [SerializeField] private Vector3 maskOffRotation = new Vector3(90, 0, 0); // away
    [SerializeField] private float rotateDuration = 0.4f;
    [SerializeField] private Ease rotateEase = Ease.OutSine;

    private Tween rotateTween;
    private bool isMaskOn = false;

    // ---------------- PUBLIC API ----------------

    public void MaskOn()
    {
        if (isMaskOn || maskPivot == null)
            return;

        RotateTo(maskOnRotation);
        isMaskOn = true;
    }

    public void MaskOff()
    {
        if (!isMaskOn || maskPivot == null)
            return;

        RotateTo(maskOffRotation);
        isMaskOn = false;
    }

    // ---------------- INTERNAL ----------------

    private void RotateTo(Vector3 targetRotation)
    {
        // Cancel previous rotation (spam safe)
        if (rotateTween != null && rotateTween.IsActive())
            rotateTween.Kill();

        rotateTween = maskPivot.DOLocalRotate(
            targetRotation,
            rotateDuration
        ).SetEase(rotateEase);
    }

    #endregion
}
