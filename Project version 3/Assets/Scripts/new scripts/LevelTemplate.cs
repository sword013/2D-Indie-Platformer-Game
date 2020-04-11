using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTemplate : MonoBehaviour
{
    public string LevelName;
    public Text LevelText;

    public void setData(string _levelName)
    {
        LevelName = _levelName;
        LevelText.text = _levelName;
        GetComponent<Button>().onClick.AddListener(() => { 
            AudioManager.instance.PlaySfx("Click");
            LevelManager.instance.LoadLevel(LevelName);
        });  // onclick listener
    }
}
