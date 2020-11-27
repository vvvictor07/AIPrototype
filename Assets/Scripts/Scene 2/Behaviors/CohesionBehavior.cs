using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Cohesion")]
    public class CohesionBehavior : LifeBehavior
    {
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // add all points together and get average
            var cohesionMove = Vector2.zero;
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

                // create offset from agent position
                // direction from a to b = b - a
                cohesionMove -= (Vector2)life.transform.position;
            }

            return cohesionMove;
        }
    }
}