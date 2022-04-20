using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    TimerManager timerManager;

    //Haewon
    public static bool IsTimeup = false;

    protected override void Awake()
    {
        base.Awake();

        timerManager = new TimerManager(30);
        timerManager.SetIsRunning(true);        
    }

    private void Start()
    {
        //Haewon
        timerManager.Timeup = false;
        IsTimeup = timerManager.Timeup;

        TimerPanelManager.instance.SetTimerText(timerManager.ToString());
    }

    private void Update()
    {
        if (timerManager.IsRunning)
        {
            timerManager.UpdateTotalTime(Time.deltaTime);
            TimerPanelManager.instance.SetTimerText(timerManager.ToString());
        }

        //Haewon
        if (timerManager.Timeup)
        {
            IsTimeup = timerManager.Timeup;
            Time.timeScale = 0f;
        }
    }
}
