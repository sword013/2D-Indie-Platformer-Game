using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        ClickSfx();
        LevelManager.instance.LoadLevel(LevelManager.instance.Levels[0]);
    }
    public void OpenSelectLevel()
    {
        ClickSfx();
        UIManager.instance.LoadLevelSelectPanel();
    }
    public void QuitGame()
    {
        ClickSfx();
        Application.Quit();
    }

    void ClickSfx()
    {
        AudioManager.instance.PlaySfx("Click");
    }
}
