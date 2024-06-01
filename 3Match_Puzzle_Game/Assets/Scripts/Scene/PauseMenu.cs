using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public static PauseMenu Instance;
    
    public static bool GameIsPaused = false;
    public GameObject _pauseMenu;

    private static string previousScene;

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임 매니저 오브젝트가 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "SettingsScene")
        { 
            Resume();
        }
        else if (GameIsPaused)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenPauseMenu()
    {
        Pause();
    }

    public void LoadGame1()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("Stage1");
    }

    public void LoadGame2()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("Stage2");
    }

    public void LoadGame3()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("Stage3");
    }

    public void GoToSettings()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("SettingsScene");
    }

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
