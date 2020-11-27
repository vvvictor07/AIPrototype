using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Avoidance")]
    public class AvoidanceBehavior : LifeBehavior
    {
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // getting average 
            var avoidanceMove = Vector2.zero;
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);
            var numAvoid = 0;

            foreach (var item in filteredContext)
            {
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.AvoidanceRadius)
                {
                    numAvoid++;
                    avoidanceMove += (Vector2)(life.transform.position - item.position);
                }
            }

            if (numAvoid > 0)
            {
                avoidanceMove /= numAvoid;
            }

            return avoidanceMove;
        }
    }
}