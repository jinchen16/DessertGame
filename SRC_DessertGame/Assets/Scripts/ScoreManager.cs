using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public ScorePanelManager scorePanelManager;
    //Haewon
    public GameOverMenu scoreGameOverMenu;
    public static bool scoreCompleted = false;
    private int targetScore = 200;
    public int Score { get; private set; }

    public void SetScore(int score)
    {
        Score = score;
        scorePanelManager.UpdateScoreText(Score);
        //Haewon
        scoreGameOverMenu.UpdateScoreText(Score);
        CompletedScore(Score);
    }

    public void IncreaseScore(int delta) => SetScore(Score + delta);

    //Haewon
    public bool CompletedScore(int score)
    {
        if (score == targetScore)
        {
            Debug.Log("Reached the target ! ");
            return scoreCompleted = true;
           
        }
        else
        {
            return scoreCompleted = false;
        }
        
    }
}
