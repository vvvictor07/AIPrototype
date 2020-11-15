using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    [System.Serializable]
    public struct BehaviorGroup
    {
        public FlockBehavior behaviors;
        public float weights;
    }

    public BehaviorGroup[] behaviors;


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        for(int i = 0; i < behaviors.Length; i++)
        {
            //gets the calculated move method of each behavior attached
            Vector2 partialMove = behaviors[i].behaviors.CalculateMove(agent, context, flock) * behaviors[i].weights;

            if(partialMove != Vector2.zero)
            {
                //check the number we get for moving the agent isnt larget than the weight given
                if(partialMove.sqrMagnitude > behaviors[i].weights * behaviors[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviors[i].weights;
                }
                //bring all the behaviors together as one
                move += partialMove;
            }
        }
        return move;
    }

}
