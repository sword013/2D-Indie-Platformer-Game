using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Vector2Int Dimensions;
    public Grid_nn GridRef;

    float lengthRadius, heightRadius;
    List<Node_nn> NodeCollisionCache, TempNodeCache, RemoveNodeCache;
    //NodeCollision has the past history(last frame)
    //TempNode has the current history (which nodes are unwalkable)
    //RemoveNodes = NodeCollision - TempNode 

    Vector3 getWorldTilesPos = new Vector3();
    Node_nn nodeCache;

    void Awake()
    {
        NodeCollisionCache = new List<Node_nn>();
        TempNodeCache = new List<Node_nn>();
        RemoveNodeCache = new List<Node_nn>(); // needed because ...can't remove an element from an array you're currently looping over :/
        lengthRadius = Dimensions.x/2f;
        heightRadius = Dimensions.y/2f;
    }

    private void Start() {
        if (GridRef == null)
        {
            GridRef = GameObject.Find("AStar").GetComponent<Grid_nn>();
        }
    }

    private void FixedUpdate() {
        UpdateTiles();
    }

    public void UpdateTiles()
    {
        TempNodeCache.Clear();
        RemoveNodeCache.Clear();
        
        for (float x = -lengthRadius; x < lengthRadius; x+=1)
        {
            for (float y = -heightRadius; y < heightRadius; y+=1)
            {
                getWorldTilesPos = gameObject.transform.position + new Vector3(x+0.8f, y+0.59f, 0); // offset values from testing and trying to get it right in the inspector

                 Debug.Log("getWorldTilesPos "+getWorldTilesPos);
                nodeCache = GridRef.NodeFromWorldPoint(getWorldTilesPos);
                TempNodeCache.Add(nodeCache);
                NodeCollisionCache.Add(nodeCache);
                nodeCache.walkable = false;
            }
        }   

        // compare the tiles that are no longer present and cache them to be removed
        foreach (Node_nn node in NodeCollisionCache)
        {
            if (!TempNodeCache.Contains(node)) // if the moving platform is no longer over a node/tile
            {
                RemoveNodeCache.Add(node);
                if (!node.isStatic) // incase of static/unwalkable background, dont make it walkable
                {
                    node.walkable = true;
                }
            }
        }

        // remove tiles that the platform is not over
        foreach (Node_nn node in RemoveNodeCache)
        {
            NodeCollisionCache.Remove(node);
        }
    }

    // //testing/debugging purpose only
    // void OnDrawGizmos()
    // {
    //     if (NodeCollisionCache != null)
    //     {
    //         foreach (Node_nn cache in NodeCollisionCache)
    //         {
    //             Gizmos.color = (cache.walkable) ? Color.white : Color.red;
    //             Gizmos.DrawCube(cache.worldPosition, Vector3.one * 0.9f);
    //         }
    //     }
    // }
}
