using System;
using DG.Tweening;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{

    public bool isGamePaused = false;
    public bool isPlayerDied = false;
    [SerializeField] PlayerInput playerInput;

    void Start()
    {
        StarterAssetsInputs.INPAC_ONBACKBUTTONPRESSED += OnBackButtonPressed;
        UIManager.Instance.FadeOutBlackScreen();
        Cursor.lockState = CursorLockMode.None;
        Application.targetFrameRate = 120;

        if(PlayerPrefs.GetInt("FirstVisitControll",0) == 1)
        {
            UIManager.Instance.ShowControlls(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OnPlayerDie()
    {
        isPlayerDied = true;
        VisionSwitcher.Instance.SetDeathVision();
        UIManager.Instance.RestartMenuOpenStatus(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnRestartButtonPressed()
    {
        UIManager.Instance.FadeInBlackScreen();

        DOVirtual.DelayedCall(1f, () =>
        {
            LevelLoader.Instance.ReloadCurrentLevel();
        });
    }

    


    public void OnGameStart()
    {
        VisionSwitcher.Instance.SetNormalVision();
    }

    public void OnLevelEnd()
    {
        UIManager.Instance.FadeInBlackScreen();

        DOVirtual.DelayedCall(1f, () =>
        {
            LevelLoader.Instance.LoadNextLevel();
        });
        
    }

    public void OnBackButtonPressed()
    {
        if(isGamePaused)
        {
            playerInput.ActivateInput();
            isGamePaused = false;
            UIManager.Instance.PauseMenuOpenStatus(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            playerInput.DeactivateInput();
            isGamePaused = true;
            UIManager.Instance.PauseMenuOpenStatus(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public void QuiteGame()
    {
        Application.Quit();
    }

    public void OnControllsButtonClick()
    {
        UIManager.Instance.ShowControlls(true);
        UIManager.Instance.PauseMenuOpenStatus(false);
        UIManager.Instance.RestartMenuOpenStatus(false);
    }

    public void OnBackFromControlls()
    {
        PlayerPrefs.SetInt("FirstVisitControll",1);
        UIManager.Instance.ShowControlls(false);
        if(isPlayerDied)
        {
            UIManager.Instance.RestartMenuOpenStatus(true);
        }else
            UIManager.Instance.PauseMenuOpenStatus(true);
    }

    public void OnMainMenuButtonPressed()
    {
         UIManager.Instance.FadeInBlackScreen();

        DOVirtual.DelayedCall(1f, () =>
        {
            LevelLoader.Instance.LoadLevel(0);
        });
    }
}
