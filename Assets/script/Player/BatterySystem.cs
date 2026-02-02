using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    [Header("Reference Battery")]
    [SerializeField] protected Image batterChunk;
    [SerializeField] protected float batterConsumeRate = 0.1f;
    [SerializeField] protected float totalBatteryPower = 1f;
    [SerializeField] protected float totalBatteryPowerCapacity = 1f;

    [Range(0f,99f)]
    [SerializeField] protected float lowBatteryPercentage =20;
    public bool isBatteryLow = false;
    [SerializeField] protected GameEvent BatteryConsumedEvent;
    protected bool isBatteryUsable = true;
    protected Action UpdateDel;
    public Action<bool> AC_batteryLowStateChanged;



    // Update is called once per frame
    void Update() => UpdateDel?.Invoke();

    protected void UpdateBattery()
    {
        if (totalBatteryPower > 0)
        {
            totalBatteryPower -= batterConsumeRate * Time.deltaTime;
            if(!isBatteryLow)
            {
                UpdateBatteryLowState();
            }

        }
        else if (isBatteryUsable)
        {
            isBatteryUsable = false;
            BatteryConsumedEvent.Raise(this, true);
        }
        batterChunk.fillAmount = totalBatteryPower;
    }
    private void UpdateBatteryLowState()
    {
        if (totalBatteryPowerCapacity <= 0f)
            return;

        float batteryPercent =
            (totalBatteryPower / totalBatteryPowerCapacity) * 100f;

        isBatteryLow = batteryPercent <= lowBatteryPercentage;

        if(isBatteryLow) AC_batteryLowStateChanged?.Invoke(true);
    }


    public bool IsBatteryLeft()
    {
        return isBatteryUsable;
    }

    public void RechargeBattery()
    {
        totalBatteryPower = 1f;
        isBatteryUsable = true;
    }
    public void RechargeBattery(float amount)
    {
        totalBatteryPower += amount;
       if(totalBatteryPower>0) isBatteryUsable = true;
       UpdateBatteryLowState();
       
       AC_batteryLowStateChanged?.Invoke(isBatteryLow); //notify

    }
    
   
}
