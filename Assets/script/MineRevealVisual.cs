using UnityEngine;
using System.Collections;

public class MineRevealVisual : MonoBehaviour
{
    [Header("Emission")]
    [SerializeField] private Color emissionColor = Color.red;
    [SerializeField] private float emissionIntensity = 3f;

    [Header("Blink")]
    [SerializeField] private float blinkSpeed = 0.5f;

    private Material materialInstance;
    private Coroutine blinkRoutine;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        materialInstance = renderer.material;

        DisableReveal();
        //EnableReveal();
    }

    public void ListenToOnInit()
    {
        
    }

    public void EnableReveal()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        blinkRoutine = StartCoroutine(BlinkEmission());
    }

    public void DisableReveal()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
            blinkRoutine = null;
        }

        materialInstance.DisableKeyword("_EMISSION");
        materialInstance.SetColor("_EmissionColor", Color.black);
    }

    private IEnumerator BlinkEmission()
    {
        materialInstance.EnableKeyword("_EMISSION");

        while (true)
        {
            materialInstance.SetColor(
                "_EmissionColor",
                emissionColor * emissionIntensity
            );

            yield return new WaitForSeconds(blinkSpeed);

            materialInstance.SetColor("_EmissionColor", Color.black);

            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public void ListenToMaskModeChange(Component sender,object data)
    {
        if((bool)data)
        {
            EnableReveal();
        }
        else
        {
            DisableReveal();
        }
    }
}
