using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1.Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
    public class CompositeBehavior : FlockBehavior
    {
        [System.Serializable]
        public struct BehaviorGroup
        {
            public FlockBehavior Behavior;

            public float Weight;
        }

        public BehaviorGroup[] Behaviors;

        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            var moveSpeed = Vector2.zero;

            // for (var i = 0; i < Behaviors.Length; i++)
            foreach (var behaviorGroup in Behaviors)
            {
                // gets the calculated move method of each behavior attached
                var partialMoveSpeed = behaviorGroup.Behavior.CalculateMoveSpeed(agent, context) * behaviorGroup.Weight;

                if (partialMoveSpeed != Vector2.zero)
                {
                    // check the number we get for moving the agent isn't larger than the weight given
                    if (partialMoveSpeed.sqrMagnitude > behaviorGroup.Weight * behaviorGroup.Weight)
                    {
                        partialMoveSpeed.Normalize();
                        partialMoveSpeed *= behaviorGroup.Weight;
                    }

                    // bring all the behaviors together as one
                    moveSpeed += partialMoveSpeed;
                }
            }

            return moveSpeed;
        }
    }
}
