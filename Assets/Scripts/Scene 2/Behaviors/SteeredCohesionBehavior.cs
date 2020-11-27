using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Steered Cohesion")]
    public class SteeredCohesionBehavior : LifeBehavior
    {
        public float AgentSmoothTime = 0.5f;

        private Vector2 currentVelocity;

        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            // if no neighbours, return no adjustment
            if (!context.Any())
            {
                return Vector2.zero;
            }

            // all add points together and average
            var cohesionMove = Vector2.zero;

            // if (filter == null) { filteredContext = context} else {filter.Filter(agent,context)}
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);
            var count = 0;

            foreach (var item in filteredContext)
            {
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.SmallRadius)
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
            cohesionMove -= (Vector2)life.transform.position;

            cohesionMove = Vector2.SmoothDamp(
                    life.transform.up,
                    cohesionMove,
                    ref currentVelocity,
                    AgentSmoothTime
                );

            return cohesionMove;
        }
    }
}