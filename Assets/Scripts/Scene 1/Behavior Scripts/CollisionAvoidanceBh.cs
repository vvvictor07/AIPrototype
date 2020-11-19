using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CollisionAvoidanceBh : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // try to filter objects
        context = Filter != null ? Filter.Filter(agent, context) : context;
        agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++

        var c = Vector2.zero;
        var minimalDistance = 2f;

        if (context.Count > 0)
        {
            foreach (var obj in context)
            {
                var distance = obj.position - agent.transform.position;

                if (distance.magnitude < minimalDistance)
                {
                    minimalDistance = distance.magnitude;
                }

                if (distance.magnitude < 2)
                {
                    c -= (Vector2)distance;
                }
            }
        }

        var part = (2f - minimalDistance) / 2f;

        c = Vector2.Lerp(Vector2.zero, c, part);

        return c;
    }
}