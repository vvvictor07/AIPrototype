using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1.Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
    public class StayInRadius : FlockBehavior
    {
        [SerializeField]
        private Vector2 center = Vector2.zero;

        [SerializeField]
        private float radius = 15;

        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            // direction to the center
            // magnitude will = distance
            var centerOffset = center - (Vector2)agent.transform.position;
            var centerOffsetMagnitude = centerOffset.magnitude / radius;

            if (centerOffsetMagnitude < 0.9f)
            {
                return Vector2.zero;
            }

            return centerOffset * centerOffsetMagnitude * centerOffsetMagnitude;
        }
    }
}
