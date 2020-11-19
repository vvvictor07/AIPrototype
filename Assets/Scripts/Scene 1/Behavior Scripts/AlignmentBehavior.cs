using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public ContextFilter Filter;
    
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        if (context.Count == 0)
        {
            // maintain same direction
            return agent.transform.up;
        }

        // add all directions together and average
        var alignmentMove = Vector2.zero;
        var filteredContext = Filter == null ? context : Filter.Filter(agent, context);

        var count = 0;

        foreach (var item in filteredContext)
        {
            // instead of context
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareSmallRadius)
            {
                alignmentMove += (Vector2)item.transform.up;
                count++;
            }
        }

        if (count != 0)
        {
            alignmentMove /= context.Count;
        }

        return alignmentMove;
    }
}