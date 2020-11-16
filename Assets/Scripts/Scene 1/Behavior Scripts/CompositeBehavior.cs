using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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

        for (var i = 0; i < Behaviors.Length; i++)
        {
            // gets the calculated move method of each behavior attached
            var partialMoveSpeed = Behaviors[i].Behavior.CalculateMoveSpeed(agent, context) * Behaviors[i].Weight;

            if (partialMoveSpeed != Vector2.zero)
            {
                // check the number we get for moving the agent isn't larger than the weight given
                if (partialMoveSpeed.sqrMagnitude > Behaviors[i].Weight * Behaviors[i].Weight)
                {
                    partialMoveSpeed.Normalize();
                    partialMoveSpeed *= Behaviors[i].Weight;
                }

                // bring all the behaviors together as one
                moveSpeed += partialMoveSpeed;
            }
        }

        return moveSpeed;
    }
}