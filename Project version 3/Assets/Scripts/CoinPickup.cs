using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour {
    [SerializeField] int pointsForCoin = 10; 
   

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        //add to score and play sound
        GameManager.instance.AddScore(pointsForCoin);
        AudioManager.instance.PlaySfx("CoinPickup");
        Destroy(gameObject);
    }
}
