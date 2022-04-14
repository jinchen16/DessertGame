using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    public float TotalTimeInSeconds { get; private set; }

    public int Seconds { get; private set; }
    public int Minutes { get; private set; }

    public bool IsRunning { get; private set; }

    public delegate void OnTimerCallback();

    public OnTimerCallback onTimerEndCallback;

    //Haewon
    public bool Timeup = false;

    public TimerManager(float totalTimeInSeconds)
    {
        SetTotalTimeInSeconds(totalTimeInSeconds);
    }

    public void SetIsRunning(bool isRunning) => IsRunning = isRunning;

    public void SetTotalTimeInSeconds(float totalTimeInSeconds)
    {
        TotalTimeInSeconds = totalTimeInSeconds;

        SetSeconds((int)TotalTimeInSeconds % 60);
        SetMinutes((int)TotalTimeInSeconds / 60);

        if (TotalTimeInSeconds <= 0)
        {
            //Haewon
            Timeup = true;
            Debug.Log("Time up !!!!!");
            SetIsRunning(false);
            SetSeconds(0);
            SetMinutes(0);
            onTimerEndCallback?.Invoke();
        }
    }

    public void SetMinutes(int minutes) => Minutes = minutes;

    public void SetSeconds(int seconds) => Seconds = seconds;

    public void UpdateTotalTime(float delta) => SetTotalTimeInSeconds(TotalTimeInSeconds - delta);

    public override string ToString()
    {
        return Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }
}
