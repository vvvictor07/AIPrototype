using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/FSMBehavior/CompositeStateBehavior")]
public class CompositeStateBehavior : StateMachineBehaviour
{
    [System.Serializable]
    public class BehaviorGroup
    {
        public FlockBehavior Behavior;

        public float Weight;
    }

    public List<BehaviorGroup> Behaviors;

    private FlockAgent flockAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flockAgent = animator.gameObject.GetComponent<FlockAgent>();

        foreach (var behaviorGroup in Behaviors)
        {
            if (behaviorGroup.Behavior != null)
            {
                // we need to create new instance of behavior for each state behavior
                behaviorGroup.Behavior = Instantiate(behaviorGroup.Behavior);
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var nearbyObjects = flockAgent.GetNearbyObjectsByRadius(flockAgent.ParentFlock.NeighborRadius);

        var totalWeight = Behaviors.Sum(behaviorGroup => behaviorGroup.Weight);

        var newVelocity = Vector2.zero;

        foreach (var behaviorGroup in Behaviors)
        {
            var partialVelocity = behaviorGroup.Behavior.CalculateMoveSpeed(flockAgent, nearbyObjects).normalized;
            partialVelocity *= flockAgent.ParentFlock.DriveFactor;
            newVelocity += (partialVelocity * behaviorGroup.Weight) / totalWeight;
        }

        // flockAgent.Velocity += Vector2.Lerp(flockAgent.Velocity, newVelocity, newVelocity.magnitude / flockAgent.ParentFlock.NeighborRadius);
        // flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, nearbyAgents.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++
        flockAgent.UpdatePosition(newVelocity);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}