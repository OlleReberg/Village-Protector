using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private AStarPathfinder pathfinder;
    private EnemyStatsSO enemyStats;
    
    void Awake()
    {
        pathfinder = GetComponent<AStarPathfinder>();
    }
    
    public void MoveToTarget(Vector3 targetPosition)
    {
        List<Node> path = pathfinder.FindPath(transform.position, targetPosition);

        // Start at the second node in the path (the first node is the current position)
        int currentNodeIndex = 1;

        // Move to the first node in the path
        Vector3 currentTarget = path[currentNodeIndex].position;
        StartCoroutine(MoveToNode(currentTarget));

        IEnumerator MoveToNode(Vector3 target)
        {
            while (transform.position != target)
            {
                // Move towards the target node
                transform.position = Vector3.MoveTowards(transform.position, target, enemyStats.MovementSpeed * Time.deltaTime);

                // If we've reached the current target node, update the target to the next node in the path
                if (transform.position == target && currentNodeIndex < path.Count - 1)
                {
                    currentNodeIndex++;
                    currentTarget = path[currentNodeIndex].position;
                }

                yield return null;
            }
        }
    }
}
