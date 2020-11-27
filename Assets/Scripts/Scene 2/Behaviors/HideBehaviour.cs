using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Scene_2.Filters;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Hide")]
    public class HideBehaviour : LifeBehavior
    {
        [SerializeField]
        private ContextFilterForLife obstaclesFilter;

        [SerializeField]
        private float hideBehindObstacleDistance = 2f;

        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            // hide from
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);

            // hide behind
            var obstacleContext = Filter == null ? context : obstaclesFilter.Filter(context, life);

            if (!filteredContext.Any() || !obstacleContext.Any())
            {
                return Vector2.zero;
            }

            // find nearest obstacle
            var nearestDistance = float.MaxValue;
            Transform nearestObstacle = null;

            foreach (var item in obstacleContext)
            {
                var distance = Vector2.Distance(item.position, life.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObstacle = item;
                }
            }

            // find best hiding spot
            var hidePosition = Vector2.zero;

            foreach (var item in filteredContext)
            {
                Vector2 obstacleDirection = nearestObstacle.position - item.position;

                obstacleDirection = obstacleDirection.normalized * hideBehindObstacleDistance;

                hidePosition += (Vector2)nearestObstacle.position + obstacleDirection;
            }

            hidePosition /= filteredContext.Count;

            Debug.DrawRay(hidePosition, Vector2.up * 1f); // debug tool to see where AI is hiding

            return hidePosition - (Vector2)life.transform.position;
        }
    }
}