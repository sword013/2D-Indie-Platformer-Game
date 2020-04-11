using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    /*
     * this is a script where we want to not destroy things while at the same level ; same like game session but
       for same levels; game session is for the whole game
     * like apan coins ghetle; it should not reappear even if you die cause you got lives(in the same level)
     * not just for coins, for any kind of pickups; they should be remembered; thus childing pickups under this
     */


    // --------------------------------------------------------- this script does nothing usefull and its terrible to check and destory the scene in UPDATE!------------------------

    // int startingBuildIndex;

    // private void Awake()
    // {
    //     int numOfScenePersists = FindObjectsOfType<ScenePersist>().Length;
    //     if (numOfScenePersists > 1)
    //         Destroy(gameObject);
    //     else
    //         DontDestroyOnLoad(gameObject);
    // }

    // private void Start()
    // {
    //     startingBuildIndex = SceneManager.GetActiveScene().buildIndex;
    // }

    // private void Update()
    // {
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     if (currentSceneIndex != startingBuildIndex)
    //         Destroy(gameObject);
    // }
}
