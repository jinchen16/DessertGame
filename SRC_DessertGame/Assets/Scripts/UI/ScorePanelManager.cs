using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _contents;

    [SerializeField]
    private TMP_Text _scoreText;

    public void UpdateScoreText(int value)
    {
        _scoreText.text = value.ToString();
    }

    public void Show()
    {
        _contents.SetActive(true);
    }

    public void Hide()
    {
        _contents.SetActive(false);
    }
}
