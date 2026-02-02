using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;


public class UIManager : MonoSingleton<UIManager>
{
    
    [SerializeField] TMP_Text logText;
    [Header("Mask UI")]
    [SerializeField] private Image maskCooldownFill;
    [SerializeField] private TMP_Text maskUsesText;
    [Header("Cooldown Shake")]
    [SerializeField] private RectTransform cooldownContainer;
    [SerializeField] private float shakeDuration = 0.4f;
    [SerializeField] private float shakeStrength = 20f;
    [SerializeField] private int shakeVibrato = 15;
    [Header("PauseMenu")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject restartPanel;
    [Header("BlackScreen")]
    [SerializeField] CanvasGroup blackScreen;
    [SerializeField] float fadeDuration =0.3f;

    void Start()
    {
        pausePanel.SetActive(false);
        //controlsPanel.SetActive(false);
        restartPanel.SetActive(false);
    }


    public void UpdateMaskCooldown(float fillAmount)
    {
        if (maskCooldownFill != null)
            maskCooldownFill.fillAmount = fillAmount;
    }

    public void UpdateMaskUses(int usesLeft)
    {
        if (maskUsesText != null)
            maskUsesText.text = usesLeft.ToString();
    }

    public void PauseMenuOpenStatus(bool open)
    {
        if(open)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    public void ShowControlls(bool show)
    {
        controlsPanel.SetActive(show);
    }

    public void ShowLogOnScreen(String data)
    {
        
    }

    public void RestartMenuOpenStatus(bool open)
    {
        restartPanel.SetActive(open);
    }



    public void FadeInBlackScreen()
    {
        blackScreen.DOFade(1,fadeDuration);
    }

    public void FadeOutBlackScreen()
    {
        blackScreen.DOFade(0,fadeDuration);
    }



     private Tween cooldownShakeTween;

    public void ShakeCooldownUI()
    {
        if (cooldownContainer == null)
            return;

        // Kill previous shake if it exists
        if (cooldownShakeTween != null && cooldownShakeTween.IsActive())
        {
            cooldownShakeTween.Kill();
        }

        // Reset position to avoid drift
        cooldownContainer.anchoredPosition = Vector2.zero;

        // Start new shake
        cooldownShakeTween = cooldownContainer.DOShakeAnchorPos(
            shakeDuration,
            shakeStrength,
            shakeVibrato,
            90f,
            false,
            true
        );
    }
    
    
    #region No Battery
    
    [Header("No Battery Blink")]
    [SerializeField] private CanvasGroup noBatteryContainer;
    [SerializeField] private int blinkCount = 3;
    [SerializeField] private float blinkDuration = 0.15f;

    private Tween noBatteryBlinkTween;

    public void BlinkNoBattery()
    {
        if (noBatteryContainer == null)
            return;

        // Kill previous blink if it exists
        if (noBatteryBlinkTween != null && noBatteryBlinkTween.IsActive())
        {
            noBatteryBlinkTween.Kill();
        }

        // Ensure visible at start
        noBatteryContainer.alpha = 1f;
        noBatteryContainer.gameObject.SetActive(true);

        // Create blink sequence
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < blinkCount; i++)
        {
            seq.Append(noBatteryContainer.DOFade(0f, blinkDuration));
            seq.Append(noBatteryContainer.DOFade(1f, blinkDuration));
        }

        // Optional: hide after blinking
        seq.OnComplete(() =>
        {
            noBatteryContainer.gameObject.SetActive(false);
        });

        noBatteryBlinkTween = seq;
    }

    #endregion
    
}
