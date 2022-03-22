using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public ScorePanelManager scorePanelManager;

    public int Score { get; private set; }

    public void SetScore(int score)
    {
        Score = score;
        scorePanelManager.UpdateScoreText(Score);
    }

    public void IncreaseScore(int delta) => SetScore(Score + delta);
}
