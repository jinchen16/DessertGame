using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    TimerManager timerManager;

    protected override void Awake()
    {
        base.Awake();

        timerManager = new TimerManager(120);
        timerManager.SetIsRunning(true);        
    }

    private void Start()
    {
        TimerPanelManager.instance.SetTimerText(timerManager.ToString());
    }

    private void Update()
    {
        if (timerManager.IsRunning)
        {
            timerManager.UpdateTotalTime(Time.deltaTime);
            TimerPanelManager.instance.SetTimerText(timerManager.ToString());
        }
    }
}
