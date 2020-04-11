using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Grid_nn : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Tilemap tilemap;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node_nn[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Start() {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Grid").transform.Find("Foreground").GetComponent<Tilemap>();
        }
    }

    void CreateGrid()
    {
        grid = new Node_nn[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                
                // bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                bool walkable = tilemap.GetTile(tilemap.WorldToCell(worldPoint)) == null ? true : false;  // checking instead if the foreground tilemap has a tile

                grid[x, y] = new Node_nn(walkable, worldPoint, x, y, walkable?false:true);
            }
        }
    }

    public List<Node_nn> GetNeighbours(Node_nn node)
    {
        List<Node_nn> neighbours = new List<Node_nn>();

        // //logic for diagonals - 8 neighbours 
        // for (int x = -1; x <= 1; x++)
        // {
        //     for (int y = -1; y <= 1; y++)
        //     {
        //         if (x == 0 && y == 0)
        //             continue;

        //         int checkX = node.gridX + x;
        //         int checkY = node.gridY + y;

        //         if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
        //         {
        //             neighbours.Add(grid[checkX, checkY]);
        //         }
        //     }
        // }


        //logic for 4 instead of 8 expected neighbours
        Vector2Int multiplyDirection = new Vector2Int(); //left right up down, no diagonals
        for (int a = 0; a < 4; a++)
        {
            // too lazy to think of a smart loop, optimize later if you feel like burning some calories
            switch (a)
            {
                case 0:
                multiplyDirection = new Vector2Int(1,0);
                break;
                case 1:
                multiplyDirection = new Vector2Int(-1,0);
                break;
                case 2:
                multiplyDirection = new Vector2Int(0,1);
                break;
                case 3:
                multiplyDirection = new Vector2Int(0,-1);
                break;
                default:
                break;
            }
            
            int checkX = node.gridX + multiplyDirection.x;
            int checkY = node.gridY + multiplyDirection.y;
            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
            {
                neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }


    public Node_nn NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2 -transform.position.x) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2 -transform.position.y) / gridWorldSize.y;

        //-transform.position.x helps with adjusting the A*'s position anywhere ..naiter ha algo phakta
        //a* = (000) wartich chalel

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    // below only for visual node representation and debugging
    public List<Node_nn> path;
    public Vector3[] waypoints; 

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (Node_nn n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (waypoints != null)
                    for (int i = 0; i < waypoints.Length; i++)
                        if (n.worldPosition == waypoints[i])
                            Gizmos.color = Color.yellow;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}