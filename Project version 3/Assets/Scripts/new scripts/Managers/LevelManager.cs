using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public string CurrentLevel;
    public List<string> Levels;
    public GameObject LevelDetails, PlayerPrefab;
    public Vector3 CurrentSpawnPoint;

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
    
    void Start() 
    {
        //load the mainmenu anyways
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            CurrentLevel = "MainMenu";
        } 
        else
        {
            LoadLevel("MainMenu");
        }
    }

    public void LoadLevel(string _level)
    {
        SceneManager.LoadScene(_level);
    }

    public void LoadNextLevel()
    {
        // string nextLevelName = CurrentLevel.Substring(0, 5) + (int.Parse(CurrentLevel.Substring(5))+1); // eg. Level10 ---> Level 10+1 ---> Level 11
        int index = Levels.IndexOf(CurrentLevel)+1;

        if (index < Levels.Count)
        {
            SceneManager.LoadScene(Levels[index]);
        }
        else
        {
            Debug.LogError("Level"+index+" does not exist!");
        }
    }

    void GetLevelData()
    {
        LevelDetails = GameObject.FindWithTag("LevelData");
        CurrentSpawnPoint = LevelDetails.transform.Find("Spawn"+0).position;
        CurrentLevel = SceneManager.GetActiveScene().name;
        //TODO: Think of a way to add checkpoint logic from here, for now leaving it at a single spawn point
    }

    void EnsurePlayerSpawned()
    {
        if (GameManager.instance.PlayerRef!= null)
        {
            GameManager.instance.PlayerRef.transform.position = CurrentSpawnPoint;
        }
        else
        {
            RespawnAtCheckpoint();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name.Contains("Level") || scene.name.Contains("level")) //TODO: find a better check to switch to ingame ui, rough work for now
        {
            GameManager.instance.ResetData();
            UIManager.instance.LoadInGamePanel();
            CurrentLevel = scene.name;
            GetLevelData();
            EnsurePlayerSpawned();

            string BGM = Random.Range(0.0f, 1.0f) > 0.5f ? "BGM1":"BGM2";
            AudioManager.instance.PlayBgm(BGM);
        }
        else if (scene.name.Contains("Menu"))
        {
            UIManager.instance.LoadMainMenuPanel();
            AudioManager.instance.PlayBgm("MainMenu");
        }
    }

    public void RespawnAtCheckpoint(){
        if (CurrentSpawnPoint == null)
        {
            Debug.LogError("Spawn Data Missing!");
            return;
        }
        GameManager.instance.PlayerRef = Instantiate(PlayerPrefab, CurrentSpawnPoint, Quaternion.identity);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }   
}
