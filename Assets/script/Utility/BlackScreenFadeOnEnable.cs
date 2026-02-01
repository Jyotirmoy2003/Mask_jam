using UnityEngine;

public class BlackScreenFadeOnEnable : MonoBehaviour
{
    [Header("Fade Settings")]
    [Tooltip("If true → Fade IN black screen, else Fade OUT")]
    public bool fadeIn = true;

    private void OnEnable()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogWarning("BlackScreenFadeOnEnable: UIManager instance not found");
            return;
        }

        if (fadeIn)
        {
            UIManager.Instance.FadeInBlackScreen();
        }
        else
        {
            UIManager.Instance.FadeOutBlackScreen();
        }
    }
}
