using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool PauseBtnIsPressed = false;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (PauseBtnIsPressed)
        {
            Pause();

        }
        
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        //Froze the game
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Debug.Log("Resume the game....");
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseBtnIsPressed = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quit the game, here is pasue menu");
        Application.Quit();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Player pressed PAUSE Button");
            PauseBtnIsPressed = true;
            Debug.Log(PauseBtnIsPressed);


        }
    }

}
