using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Flocking : StateMachineBehaviour
{
    private GameObject NPC;

    private FlockAgent flockAgent;

    private ContextFilter filter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        flockAgent = NPC.GetComponent<FlockAgent>();
        
        // filter = CreateInstance<DifferentFlockFilter>();
        filter = CreateInstance<SameFlockFilter>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var agents = GetNearbyObjectsByRadius(flockAgent, flockAgent.ParentFlock.NeighborRadius);

        agents = agents.Where(x => x.ParentFlock == flockAgent.ParentFlock).ToList();

        flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, agents.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++

        var calculatedCohesionVelocity = CalculateCohesionVelocity(flockAgent, agents);
        calculatedCohesionVelocity *= 5f;

        var calculatedDistanceVelocity = CalculateDistanceVelocity(flockAgent, agents);

        flockAgent.Velocity += calculatedCohesionVelocity;
        flockAgent.Velocity += calculatedDistanceVelocity;
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

    private Vector2 CalculateDistanceVelocity(FlockAgent currentAgent, List<FlockAgent> otherAgents)
    {
        var c = Vector2.zero;

        if (otherAgents.Count > 0)
        {
            foreach (var agent in otherAgents)
            {
                var distance = agent.transform.position - currentAgent.transform.position;

                if (distance.magnitude < 2)
                {
                    c -= (Vector2)distance;
                }
            }
        }

        return c;
    }

    private Vector2 CalculateCohesionVelocity(FlockAgent currentAgent, List<FlockAgent> otherAgents)
    {
        // all add points together and average
        var average = Vector2.zero;

        // var count = 0;

        if (otherAgents.Count > 0)
        {
            foreach (var agent in otherAgents)
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

            average /= otherAgents.Count;
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

    private List<FlockAgent> GetNearbyObjectsByRadius(FlockAgent agent, float radius)
    {
        return Physics2D
            .OverlapCircleAll(agent.transform.position, radius)
            .Where(x => x != agent.AgentCollider)
            .Select(x => x.GetComponent<FlockAgent>())
            .Where(x => x != null)
            .ToList();
    }
}