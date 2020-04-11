using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public Text livesText, scoreText;
    public GameObject GameOverText;

    void Start() {
        HideGameOverText();
    }

    public void DisplayGameOverText()
    {
        GameOverText.SetActive(true);
    }
    public void HideGameOverText()
    {
        GameOverText.SetActive(false);
    }
}
