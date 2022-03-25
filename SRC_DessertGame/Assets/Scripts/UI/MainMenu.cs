using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayContents;

    [SerializeField]
    private GameObject _mainMenuContents;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void PlayControlsTutorial()
    {
        _mainMenuContents.SetActive(false);
        _howToPlayContents.SetActive(true);
    }

    public void OnOptionsBtnPressed()
    {
        _mainMenuContents.SetActive(false);
    }
}
