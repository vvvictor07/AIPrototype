using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Flocking : StateMachineBehaviour
{
    [System.Serializable]
    public class BehaviorGroup
    {
        public FlockBehavior Behavior { get; set; }

        public float Weight { get; set; }
    }

    public List<BehaviorGroup> Behaviors; 
    
    private GameObject NPC;

    private FlockAgent flockAgent;

    // private ContextFilter filter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        flockAgent = NPC.GetComponent<FlockAgent>();
        
        // filter = CreateInstance<DifferentFlockFilter>();
        // filter = CreateInstance<SameFlockFilter>();

        Behaviors = new List<BehaviorGroup>
                        {
                            new BehaviorGroup{Behavior = CreateInstance<CohesionBh>().Init(CreateInstance<SameFlockFilter>()), Weight = 1f},
                            new BehaviorGroup{Behavior = CreateInstance<CollisionAvoidanceBh>().Init(CreateInstance<SameFlockFilter>()), Weight = 1f},
                        };
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var nearbyObjects = flockAgent.GetNearbyObjectsByRadius(flockAgent.ParentFlock.NeighborRadius);

        var totalWeight = Behaviors.Sum(behaviorGroup => behaviorGroup.Weight);

        foreach (var behaviorGroup in Behaviors)
        {
            var partialVelocity = behaviorGroup.Behavior.CalculateMoveSpeed(flockAgent, nearbyObjects);
            flockAgent.Velocity += partialVelocity * behaviorGroup.Weight / totalWeight;
        }

        // filtration 
        // var nearbyAgents = nearbyObjects.Where(x => x.GetComponent<FlockAgent>()?.ParentFlock == flockAgent.ParentFlock).ToList();
        // var nearbyAgents = filter != null ? filter.Filter(flockAgent, nearbyObjects);

        // flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, nearbyObjects.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++

        // var calculatedCohesionVelocity = CalculateCohesionVelocity(flockAgent, nearbyObjects);
        // calculatedCohesionVelocity *= 5f;
        //
        // var calculatedDistanceVelocity = CalculateDistanceVelocity(flockAgent, nearbyObjects);
        //
        // flockAgent.Velocity += calculatedCohesionVelocity;
        // flockAgent.Velocity += calculatedDistanceVelocity;
        flockAgent.UpdatePosition();

        // flockAgent.Move(calculatedMoveSpeed);
        // flockAgent.transform.up = calculatedMoveSpeed;
        // flockAgent.transform.position += (Vector3)flockAgent.Velocity * Time.deltaTime;
        // flockAgent.transform.up = (Vector3)flockAgent.Velocity;
        // flockAgent.transform.rotation = Quaternion.LookRotation((Vector3)calculatedVelocity);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    private Vector2 CalculateDistanceVelocity(FlockAgent currentAgent, List<Transform> otherObjects)
    {
        var c = Vector2.zero;

        if (otherObjects.Count > 0)
        {
            foreach (var obj in otherObjects)
            {
                var distance = obj.position - currentAgent.transform.position;

                if (distance.magnitude < 2)
                {
                    c -= (Vector2)distance;
                }
            }
        }

        return c;
    }

    private Vector2 CalculateCohesionVelocity(FlockAgent currentAgent, List<Transform> otherObjects)
    {
        // all add points together and average
        var average = Vector2.zero;

        // var count = 0;

        if (otherObjects.Count > 0)
        {
            foreach (var agent in otherObjects)
            {
                // var diff = agent.transform.position - currentAgent.transform.position;
                // instead of context
                // if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareSmallRadius)
                // if (diff.magnitude < agent.ParentFlock.NeighborRadius)
                // {
                average += (Vector2)agent.transform.position;
                // count += 1;
                // }
            }

            average /= otherObjects.Count;
            average -= (Vector2)currentAgent.transform.position;
            average /= 100;

            // average = Vector2.Lerp(Vector2.zero, average, average.magnitude / agent.ParentFlock.NeighborRadius);
            // average += Vector2.Lerp(Vector2.zero, average, 0.01f);
        }

        // create offset from agent position
        // cohesionMove -= (Vector2)agent.transform.position;

        // cohesionMove = Vector2.SmoothDamp(
        //         agent.transform.up,
        //         cohesionMove,
        //         ref currentVelocity,
        //         0.001f
        //     );

        return average;
    }

    // private List<FlockAgent> GetNearbyFlockAgentsByRadius(FlockAgent agent, float radius)
    // {
    //     return Physics2D
    //         .OverlapCircleAll(agent.transform.position, radius)
    //         .Where(x => x != agent.AgentCollider)
    //         .Select(x => x.GetComponent<FlockAgent>())
    //         .Where(x => x != null)
    //         .ToList();
    // }
    //
    // private List<Transform> GetNearbyObjectsByRadius(FlockAgent agent, float radius)
    // {
    //     return Physics2D
    //         .OverlapCircleAll(agent.transform.position, radius)
    //         .Where(x => x != agent.AgentCollider)
    //         .Select(x => x.transform)
    //         .ToList();
    // }
}