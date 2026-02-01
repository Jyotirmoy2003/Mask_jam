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
}
