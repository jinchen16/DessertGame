using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        GameOverMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimeup)
        {
            GameOverMenuUI.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("Haewon_tests");
    }
}
