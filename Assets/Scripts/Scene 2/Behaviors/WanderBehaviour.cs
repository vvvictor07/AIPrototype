using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Wander")]
    public class WanderBehaviour : LifeBehavior
    {
        private Path path = null;

        private int currentWaypoint = 0;

        private Vector2 waypointDirection = Vector2.zero;

        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (path == null)
            {
                FindPath(life, context);
            }

            return FollowPath(life);
        }

        public bool InRadius(Life agent)
        {
            waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

            return waypointDirection.magnitude < path.radius;
        }

        public Vector2 FollowPath(Life life)
        {
            if (path == null)
            {
                return Vector2.zero;
            }

            if (InRadius(life))
            {
                currentWaypoint++; // go to next waypoint

                if (currentWaypoint >= path.waypoints.Count)
                {
                    // if at last waypoint
                    currentWaypoint = 0; // reset waypoint
                }

                return Vector2.zero; // dont have to move if at waypoint already
            }

            return waypointDirection; // return class variable
        }

        public void FindPath(Life life, List<Transform> context)
        {
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);

            if (filteredContext.Count == 0)
            {
                return;
            }

            var randomPath = Random.Range(0, filteredContext.Count);
            path = filteredContext[randomPath].GetComponentInParent<Path>();
        }
    }
}