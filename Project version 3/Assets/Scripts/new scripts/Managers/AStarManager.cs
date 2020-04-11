using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager instance;
    public Grid_nn grid;
    public Pathfinding pathfinding;

    private void Awake() {
        // if (instance != null)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
    }

    void Start()
    {
        
    }

}
