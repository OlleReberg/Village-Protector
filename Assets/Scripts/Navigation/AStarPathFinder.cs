using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinder : MonoBehaviour {

    private NewGrid grid;

    void Awake() 
    {
        grid = GetComponent<NewGrid>();
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos) 
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) 
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbors(currentNode)) 
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour)) 
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) 
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) 
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode) 
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) 
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
    }

    int GetDistance(Node nodeA, Node nodeB) 
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY) 
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}