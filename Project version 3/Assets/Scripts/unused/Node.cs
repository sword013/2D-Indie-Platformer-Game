using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

    public bool walkable;
    public Vector3 worldPosition;
    
   

    //Node la value set karnara function
    public Node(bool _walkable, Vector3 _worldPosition)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
    

    }

    
}
