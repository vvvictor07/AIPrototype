using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // getting average 
        var avoidanceMove = Vector2.zero;
        var numAvoid = 0;
        var filteredContext = Filter == null ? context : Filter.Filter(agent, context);

        foreach (var item in filteredContext)
        {
            // instead of context
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareAvoidanceRadius)
            {
                numAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }

        return avoidanceMove;
    }
}