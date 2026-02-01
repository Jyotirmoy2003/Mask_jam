using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 100;

    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float upwardModifier = 1.5f;
    [SerializeField] private LayerMask affectedLayers;
    [Space]
    [Header("Sound Settings")]
    [SerializeField] AudioSource beepSound;
    [Range(1f,5f)]
    [SerializeField] float beepSoundInterval;

    [Header("VFX")]
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private float destroyDelay = 0.1f;
    [SerializeField] List<GameObject> lindMineMeshes = new List<GameObject>();

    private bool hasExploded = false;

    void Start()
    {
        InvokeRepeating(nameof(PlayBeepSound),beepSoundInterval,beepSoundInterval);
        HideMine();
    }

    void PlayBeepSound()
    {
        if(!hasExploded)
        {
            beepSound?.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasExploded)
            return;

        // Only trigger once (usually player)
        if (!other.CompareTag("Player"))
            return;

        Explode();
    }

    private void Explode()
    {
        hasExploded = true;

        // Spawn VFX
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }


       

        // Detect everything in radius
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            affectedLayers
        );

        foreach (Collider col in colliders)
        {
            
            // Damage
            IDamageable damageable = col.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            // Physics force
            Rigidbody rb = col.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(
                    explosionForce,
                    transform.position,
                    explosionRadius,
                    upwardModifier,
                    ForceMode.Impulse
                );
            }
        }

        Destroy(gameObject, destroyDelay);
    }

    // Visualize explosion radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void ListenToOnMaskModeChange(Component sender,object data)
    {
        if((bool)data)
        {
            ShowMine();
        }
        else
        {
            HideMine();
        }
    }

    void ShowMine()
    {
        foreach (GameObject item in lindMineMeshes)
        {
            item.SetActive(true);
        }
    }

    void HideMine()
    {
        foreach (GameObject item in lindMineMeshes)
        {
            item.SetActive(false);
        }
    }
}
