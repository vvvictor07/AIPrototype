using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and get average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (Filter == null) ? context : Filter.Filter(agent, context);
        int count = 0;
        foreach (Transform item in filteredContext) //instead of context
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareSmallRadius)
            {
                cohesionMove += (Vector2)item.position;
                count++;
            }
                
        }
        if(count != 0)
        {
            cohesionMove /= count;
        }
        

        //create offset from agent position
        //direction from a to b = b - a
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }
}
