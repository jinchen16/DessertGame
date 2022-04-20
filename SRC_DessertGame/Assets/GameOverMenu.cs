using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverMenuUI;
    public GameObject CompletedMenuUI;

    [SerializeField]
    private TMP_Text _scoreText;

// Start is called before the first frame update
    void Start()
    {
        GameOverMenuUI.SetActive(false);
        CompletedMenuUI.SetActive(false);
    }


    public void UpdateScoreText(int value)
    {
        _scoreText.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimeup && !(ScoreManager.scoreCompleted))
        {
            GameOverMenuUI.SetActive(true);

        }
        if (ScoreManager.scoreCompleted)
        {
            CompletedMenuUI.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {

        SceneManager.LoadScene("SampleScene");
    }
}
