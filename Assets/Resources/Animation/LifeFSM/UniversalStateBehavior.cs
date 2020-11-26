using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Scene_2;
using Assets.Scripts.Scene_2.Behaviors;

using UnityEngine;

namespace Assets.Resources.Animation.LifeFSM
{
    [CreateAssetMenu(menuName = "Life/StateBehaviors/UniversalStateBehavior")]
    public class UniversalStateBehavior : StateMachineBehaviour
    {
        [System.Serializable]
        public class BehaviorGroup
        {
            public LifeBehavior Behavior;

            public float Weight;
        }

        [SerializeField]
        public List<BehaviorGroup> Behaviors;

        private Life life;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            life = animator.gameObject.GetComponent<Life>();

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
            var nearbyObjects = life.GetNearbyObjectsByRadius(life.Characteristics.NeighborRadius);

            var totalWeight = Behaviors.Sum(behaviorGroup => behaviorGroup.Weight);

            var newVelocity = Vector2.zero;

            foreach (var behaviorGroup in Behaviors)
            {
                var partialVelocity = behaviorGroup.Behavior.CalculateMoveSpeed(life, nearbyObjects).normalized;
                partialVelocity *= life.Characteristics.DriveFactor;
                newVelocity += partialVelocity * (behaviorGroup.Weight / totalWeight);
            }

            // flockAgent.Velocity += Vector2.Lerp(flockAgent.Velocity, newVelocity, newVelocity.magnitude / flockAgent.ParentFlock.NeighborRadius);
            // flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, nearbyAgents.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++
            life.UpdatePosition(newVelocity);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}