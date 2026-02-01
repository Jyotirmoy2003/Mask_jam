using UnityEngine;
using System.Collections;
using StarterAssets;

public class MaskController : BatterySystem
{
    [Header("Mask Settings")]
    public int maxUses = 3;
    [SerializeField] private float revealDuration = 5f;
    [SerializeField] private float cooldownDuration = 10f;
    [SerializeField] GameEvent Evnet_OnMaskModeChanged;
    [Header("Reference Night Vision")]
    [SerializeField] GameObject nightVisionOverlay;
    public bool haveNightVision = false;

    private int remainingUses;
    private bool isOnCooldown = false;
    private bool isActive = false;

    private void Start()
    {
        remainingUses = maxUses;
        UIManager.Instance.UpdateMaskUses(remainingUses);
        UIManager.Instance.UpdateMaskCooldown(1f);
        StarterAssetsInputs.INPAC_ONMASKBUTTONPRESSED += ToggleMask;
    }

    void OnDisable()
    {
        StarterAssetsInputs.INPAC_ONMASKBUTTONPRESSED -= ToggleMask;
    }



    void MaskVisionUpdate()
    {
        UpdateBattery();
    }

    void ToggleMask()
    {
       
        isActive = !isActive;
        AcitvateMaskVision(isActive);
    }

   
    
    public void AcitvateMaskVision(bool IsActive)
    {
        if (IsActive && isBatteryUsable)
        {
             if(isOnCooldown)
            {
                //    UIManager.Instance.ShowLogOnScreen("On Cool Down..");
                return;
            }
            // cam.fieldOfView=60;
            // zoomBar.fillAmount=0.6f;
            UpdateDel = MaskVisionUpdate;
            nightVisionOverlay.SetActive(true);
            
            VisionSwitcher.Instance.SetMaskVision();
            Evnet_OnMaskModeChanged?.Raise(this,true); //notify
            StartCoroutine(CooldownRoutine());
        }
        else
        {
            // cam.fieldOfView=60;
            // zoomBar.fillAmount=0.6f;
            UpdateDel = null;
            nightVisionOverlay.SetActive(false);

            VisionSwitcher.Instance.SetNormalVision();
            Evnet_OnMaskModeChanged?.Raise(this,false);
        }
    }

    #region  EVENT
    public void ListenToBatteryConsumed(Component sender, object data)
    {

        if ((bool)data && !isBatteryUsable)
        {
            AcitvateMaskVision(false);

        }
    }



    
    #endregion



    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        float timer = 0f;

        while (timer < cooldownDuration)
        {
            timer += Time.deltaTime;
            float fill = timer / cooldownDuration;
            UIManager.Instance.UpdateMaskCooldown(fill);
            yield return null;
        }

        UIManager.Instance.UpdateMaskCooldown(1f);
        isOnCooldown = false;
    }




}
