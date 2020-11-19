using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Hide")]
public class HideBehaviour : FilteredFlockBehavior
{
    public ContextFilter Filter;

    public ContextFilter obstaclesFilter;

    public float hideBehindObstacleDistance = 2f;

    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // hide from
        var filteredContext = Filter == null ? context : Filter.Filter(agent, context);

        // hide behind
        var obstacleContext = Filter == null ? context : obstaclesFilter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        // find nearest obstacle
        var nearestDistance = float.MaxValue;
        Transform nearestObstacle = null;

        foreach (var item in obstacleContext)
        {
            var distance = Vector2.Distance(item.position, agent.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObstacle = item;
            }
        }

        // if no obstacle
        if (nearestObstacle == null)
        {
            return Vector2.zero;
        }

        // find best hiding spot
        var hidePosition = Vector2.zero;

        foreach (var item in filteredContext)
        {
            Vector2 obstacleDirection = nearestObstacle.position - item.position;

            obstacleDirection = obstacleDirection.normalized * hideBehindObstacleDistance;

            hidePosition += (Vector2)nearestObstacle.position + obstacleDirection;
        }

        hidePosition /= filteredContext.Count;

        Debug.DrawRay(hidePosition, Vector2.up * 1f); // debug tool to see where AI is hiding

        return hidePosition - (Vector2)agent.transform.position;
    }
}