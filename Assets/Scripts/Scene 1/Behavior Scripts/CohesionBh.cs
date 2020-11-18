using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CohesionBh : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // try to filter objects
        context = Filter != null ? Filter.Filter(agent, context) : context;

        var c = Vector2.zero;

        if (context.Count > 0)
        {
            foreach (var obj in context)
            {
                var distance = obj.position - agent.transform.position;

                if (distance.magnitude < 2)
                {
                    c -= (Vector2)distance;
                }
            }
        }

        return c;
    }
}