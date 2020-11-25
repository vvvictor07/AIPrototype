using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Pursuit")]
public class PursuitBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        var filteredContext = Filter == null ? context : Filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        var move = Vector2.zero;

        foreach (var item in filteredContext)
        {
            var distance = Vector2.Distance(item.position, agent.transform.position);
            var disancePercent = distance / agent.ParentFlock.NeighborRadius;
            var inverseDistancePercent = 1 - disancePercent;
            var weight = inverseDistancePercent / filteredContext.Count;

            Vector2 direction = (item.position - agent.transform.position) * weight;

            move += direction;
        }

        return move;
    }
}