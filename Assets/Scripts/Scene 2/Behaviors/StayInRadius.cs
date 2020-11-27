using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Stay In Radius")]
    public class StayInRadius : LifeBehavior
    {
        [SerializeField]
        private Vector2 center = Vector2.zero;

        [SerializeField]
        private float radius = 15;

        public override Vector2 CalculateMoveSpeed(Life agent, List<Transform> context)
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
