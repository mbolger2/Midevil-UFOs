using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    //The name of the input button that will pause the game
    public string pauseInputName;

    //the name of the main menu scene
    public string mainMenu;

    //A static variable to keep track of wheter the gameis paused
    public static bool isPaused = false;

    //The object with the pause menu
    public GameObject pauseMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(pauseInputName))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPause();
            }
        }
    }

    //the function that will pause the game
    public void PauseGame()
    {
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //the function that will unpause the game
    public void UnPause()
    {
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
