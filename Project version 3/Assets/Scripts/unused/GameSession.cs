using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    int score = 0; 
  

    [SerializeField] Text livesText, scoreText;
    //ui canvas is childed under score; so text will not be destroyed too


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        //dont allow the creation of a new game object if one already running; this is the concept of
        //SINGLETON : let only one thing exist at a time; dont allow others of same kind 
        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

        //else just keep this object alive throughout the game; even when another scenes load up
    }

    public void ProcessPlayerDeath()
    {
        //StartMyRoutine(); 
        //task remaining : add delay before loading any level
        if (playerLives > 1)
            TakeLife();
        else
            ResetGameSession();
    }

    private void ResetGameSession()
    {
        score = 0; 
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentBuildIndex);
        livesText.text = playerLives.ToString();
    }


    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }


    /* private void StartMyRoutine()
     {
         StartCoroutine(Wait());
     }

     IEnumerator Wait()
     {
         yield return new WaitForSecondsRealtime(5);
     }*/

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }
}
