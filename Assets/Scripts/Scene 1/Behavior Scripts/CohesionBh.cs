using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CohesionBh : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // try to filter objects
        context = Filter != null ? Filter.Filter(agent, context) : context;
        // agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++


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
            // average -= (Vector2)agent.transform.position;
            average = Vector2.Lerp(Vector2.zero, average, average.magnitude / agent.ParentFlock.NeighborRadius);

        }

        return average;
    }
}