using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_nn {
    public bool walkable;
    public bool isStatic; // to check if static background object or moveable platforms
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node_nn parent;

    public Node_nn(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, bool _isStatic)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        isStatic = _isStatic;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
