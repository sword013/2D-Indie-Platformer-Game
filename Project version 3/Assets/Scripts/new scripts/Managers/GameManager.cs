// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Score = 0, PlayerLives = 3;
    public GameObject PlayerRef;

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
    }

    void Start() {
        PlayerRef = GameObject.FindGameObjectWithTag("Player"); // incase of null value during testing
    }

    public void AddScore(int _score)
    {
        Score += _score;
        UIManager.instance.DisplayScore(Score);
    }

    public void ResetData()
    {
        Score = 0;
        PlayerLives = 3;
        UIManager.instance.DisplayScore(Score);
        UIManager.instance.DisplayLives(PlayerLives);
    }
    
    public void PlayerDeath(){
        if (PlayerLives > 0)
        {
            PlayerLives--;
            UIManager.instance.DisplayLives(PlayerLives);
            AudioManager.instance.PlaySfx("Death");
            Invoke("Respawn", 2f);
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private void Respawn()
    {
        Destroy(GameObject.FindWithTag("Player")); // not the best way, but too lazy to code :P
        LevelManager.instance.RespawnAtCheckpoint();
    }

    IEnumerator GameOver()
    {
        AudioManager.instance.StopBgm();
        string GameOverSound = Random.Range(0.0f, 1.0f) > 0.5f ? "GameOver1":"GameOver1";
        AudioManager.instance.PlaySfx(GameOverSound);

        UIManager.instance.ShowGameOver();
        yield return new WaitForSeconds(4);
        UIManager.instance.HideGameOver();
        LevelManager.instance.LoadLevel("MainMenu");
    }

}
