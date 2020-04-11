using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainMenuPanel, LevelSelectPanel, InGamePanel;
    List<GameObject> allPanels;
    MainMenu MainMenu;
    LevelSelect LevelSelect;
    InGame InGame;
    
    //singleton
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        allPanels = new List<GameObject>();
        allPanels.Add(MainMenuPanel);
        allPanels.Add(LevelSelectPanel);
        allPanels.Add(InGamePanel);

        MainMenu = MainMenuPanel.GetComponent<MainMenu>();
        LevelSelect = LevelSelectPanel.GetComponent<LevelSelect>();
        InGame = InGamePanel.GetComponent<InGame>();
    }


    // ------------------------- InGame UI Updates ---------------------------
    public void DisplayLives(int _lives){
        InGame.livesText.text = "Lives : "+ _lives;
    }
    public void DisplayScore(int _score){
        InGame.scoreText.text = "Score : "+ _score;
    }
    public void ShowGameOver()
    {
        InGame.DisplayGameOverText();
    }
    public void HideGameOver()
    {
        InGame.HideGameOverText();
    }
    public void JumpBtn()
    {
        GameManager.instance.PlayerRef.GetComponent<Player>().BtnJump();
    }
    // ---------------------------------------------------------------

    // ------------------------ Changing UI states -------------------
    public void LoadMainMenuPanel()
    {
        CloseAllPanels();
        MainMenuPanel.SetActive(true);
    }
    public void LoadLevelSelectPanel()
    {
        CloseAllPanels();
        LevelSelectPanel.SetActive(true);
    }
    public void LoadInGamePanel()
    {
        CloseAllPanels();
        InGamePanel.SetActive(true);
    }
    void CloseAllPanels()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }
    }
    // -----------------------------------------------------------------
    
}
