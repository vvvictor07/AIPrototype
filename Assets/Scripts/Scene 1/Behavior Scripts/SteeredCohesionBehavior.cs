using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    private Vector2 currentVelocity;

    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // all add points together and average
        var cohesionMove = Vector2.zero;

        // if (filter == null) { filteredContext = context} else {filter.Filter(agent,context)}
        var filteredContext = Filter == null ? context : Filter.Filter(agent, context);
        var count = 0;

        foreach (var item in filteredContext)
        {
            // instead of context
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareSmallRadius)
            {
                cohesionMove += (Vector2)item.position;
                count++;
            }
        }

        if (count != 0)
        {
            cohesionMove /= count;
        }

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        cohesionMove = Vector2.SmoothDamp(
                agent.transform.up,
                cohesionMove,
                ref currentVelocity,
                agentSmoothTime
            );
        return cohesionMove;
    }
}