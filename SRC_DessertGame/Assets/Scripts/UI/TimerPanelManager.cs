using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerPanelManager : Singleton<TimerPanelManager>
{
    [Header("Panel properties")]
    [SerializeField]
    private GameObject _contents;

    [SerializeField]
    private TMP_Text _timerText;

    public void Show()
    {
        _contents.SetActive(true);
    }

    public void Hide()
    {
        _contents.SetActive(false);
    }

    public void SetTimerText(string text) => _timerText.text = text;
}
