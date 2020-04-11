using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_n : MonoBehaviour
{

    [SerializeField] Transform player; Node playerNode;

    //2d array of nodes which is grid in our case
    Node[,] grid;

    //area that the grid is going to cover in the world
    [SerializeField] Vector2 gridWorldSize;

    //space that each node covers
    [SerializeField] float nodeRadius;

    [SerializeField] LayerMask unwalkableMask;

    private void OnDrawGizmos()
    {
        //drawing the area
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y,1 ));

       

        //drawing the nodes
        if (grid != null)
        {
            playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid)
            {
                //for each node, draw a cube and set color red for obstacles and other for not

               
                if (n == playerNode)
                {
                    Gizmos.color = Color.blue;
                }
                else if (n.walkable)
                {
                    Gizmos.color = Color.white;
                }
                
                else
                {
                    Gizmos.color = Color.red;
                }



                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.05f));
                // 0.01 is subtracted to give the gaps between cubes 
            }
        }
    }


    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //no of nodes in X direction
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
       
        
    }


    /*private void Update()
    {
        CreateGrid(); //for moving ground
    }*/

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2
                                  - Vector3.up * gridWorldSize.y / 2;

        Debug.Log(worldBottomLeft);

        /* 
         Vector3.right   = (1,0,0)
         Vector3.forward = (0,0,1)
         Vector3.up = (0,1,0)
         */


        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius)
                                    + Vector3.up * (j * nodeDiameter + nodeRadius);

                //so this is your new point. Do the collision check for that point ! 
                //checksphere returns true if there is any collision, so walkable if not collision

                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));

                //create the node finally





                grid[i, j] = new Node(walkable, worldPoint);
            }

        }

    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {

        /*worldPosition is the position of any object that would be passed here  
        In return, you get the node that the object stands on and all nodes are present in grid array . 
        This object could be our player*/

       
        //percentX = 1 means far on right ; =0.5 means in the middle

        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
      
        //taking precaution ki if our player is outside the world for whatever reason, naito ill give errors
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //arrays start from 0, so -1
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

       
        return grid[x, y];
    }



}
