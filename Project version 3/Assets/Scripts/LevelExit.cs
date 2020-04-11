using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    public float levelLoadDelay = 5f;
    public float levelExitSlowMotion =0.2f ;
    bool CoroutineIsActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CoroutineIsActive && collision.gameObject.GetComponent<Player>().isAlive)
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        CoroutineIsActive = true;
        Time.timeScale = levelExitSlowMotion;  //slow down the time 0-1
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;  //return back to original time
        CoroutineIsActive = false;
        LevelManager.instance.LoadNextLevel();
        
    }
}
