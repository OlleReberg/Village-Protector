using UnityEngine;
public class Node
{
    public bool isWalkable;
    public Vector3 position;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public Node parent;

    public Node(bool walkable, Vector3 pos, int x, int y)
    {
        isWalkable = walkable;
        position = pos;
        gridX = x;
        gridY = y;
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
