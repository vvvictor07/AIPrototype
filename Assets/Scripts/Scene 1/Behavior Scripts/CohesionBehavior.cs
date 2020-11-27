using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1.Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
    public class CohesionBehavior : FilteredFlockBehavior
    {
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // add all points together and get average
            var cohesionMove = Vector2.zero;
            var filteredContext = Filter == null ? context : Filter.Filter(agent, context);
            var count = 0;

            foreach (var item in filteredContext)
            {
                // instead of context
                if (Vector2.Distance(item.position, agent.transform.position) <= agent.ParentFlock.SmallRadius)
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
            // direction from a to b = b - a
            cohesionMove -= (Vector2)agent.transform.position;
            return cohesionMove;
        }
    }
}