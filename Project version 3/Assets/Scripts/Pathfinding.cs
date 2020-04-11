using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour{

    Transform seeker, target;
    public Grid_nn grid;
    bool onlyonce = true;
    GameObject seekerObject;
    public float speed = 10f;
    

    void Awake()
    {
        // grid = GetComponent<Grid_nn>();
    }

    private void Start() {
        seekerObject = gameObject;
        seeker = gameObject.transform;
        if (grid == null)
        {
            grid = GameObject.Find("AStar").GetComponent<Grid_nn>();
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = GameManager.instance.PlayerRef.transform;
        }
        FindPath(seeker.position, target.position); 
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node_nn startNode = grid.NodeFromWorldPoint(startPos);
        Node_nn targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode == targetNode)
        {
            return;
        }

        List<Node_nn> openSet = new List<Node_nn>();
        HashSet<Node_nn> closedSet = new HashSet<Node_nn>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node_nn node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node_nn neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node_nn startNode, Node_nn endNode)
    {
        List<Node_nn> path = new List<Node_nn>();
        Node_nn currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        // Vector3[] waypoints = SimplifyPath(path);
        Vector3[] waypoints = GetLastNodeToFollow(path); // easier and more accurate way
        
        Array.Reverse(waypoints);

        grid.path = path;
        grid.waypoints = waypoints;


        if (onlyonce)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                // print("Point no");
                // print(i);
                // print("=");
                // print(waypoints[i]);
               
            }
            onlyonce = false;
        }
        
        if (waypoints.Length > 0)
        {
            seekerObject.transform.position = Vector3.MoveTowards(seekerObject.transform.position, waypoints[0], speed*Time.deltaTime);
            float dir = seekerObject.transform.position.x - waypoints[0].x;

            if (dir < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
    }

    // a bit buggy and seeker can get stuck in stuff
    Vector3[] SimplifyPath(List<Node_nn> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }
    // easier method, returns only the start node so the seeker can move towards starting node in the path always
    Vector3[] GetLastNodeToFollow(List<Node_nn> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        waypoints.Add(path[path.Count - 1].worldPosition);
        return waypoints.ToArray();
    }

    int GetDistance(Node_nn nodeA, Node_nn nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
