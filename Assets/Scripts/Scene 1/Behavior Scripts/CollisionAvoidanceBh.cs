using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CollisionAvoidanceBh : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // try to filter objects
        context = Filter != null ? Filter.Filter(agent, context) : context;

        // all add points together and average
        var average = Vector2.zero;

        // var count = 0;

        if (context.Count > 0)
        {
            foreach (var obj in context)
            {
                average += (Vector2)obj.position;
            }

            average /= context.Count;
            average -= (Vector2)agent.transform.position;
            average /= 100;
        }

        return average;
    }
}