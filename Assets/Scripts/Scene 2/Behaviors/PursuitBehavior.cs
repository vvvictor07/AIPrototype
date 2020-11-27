using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Pursuit")]
    public class PursuitBehavior : LifeBehavior
    {
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);

            if (!filteredContext.Any())
            {
                return Vector2.zero;
            }

            var move = Vector2.zero;

            foreach (var item in filteredContext)
            {
                var distance = Vector2.Distance(item.position, life.transform.position);
                var disancePercent = distance / life.Characteristics.NeighborRadius;
                var inverseDistancePercent = 1 - disancePercent;
                var weight = inverseDistancePercent / filteredContext.Count;

                Vector2 direction = (item.position - life.transform.position) * weight;

                move += direction;
            }

            return move;
        }
    }
}