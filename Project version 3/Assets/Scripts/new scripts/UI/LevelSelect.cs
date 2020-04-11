using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject ScrollContent, LevelTemplatePrefab;
    List<GameObject> LevelsAvailable;

    void Start() {
        foreach (string level in LevelManager.instance.Levels)
        {
            GameObject go = Instantiate(LevelTemplatePrefab, Vector3.zero, Quaternion.identity, ScrollContent.transform); // instantiate and parent to scroll
            go.GetComponent<LevelTemplate>().setData(level); // set name, text and onclick listener
        }    
    }
}
