using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Flocking : StateMachineBehaviour
{
    private GameObject NPC;

    private FlockAgent flockAgent;

    private ContextFilter filter;

    private Vector2 currentVelocity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        flockAgent = NPC.GetComponent<FlockAgent>();
        filter = CreateInstance<DifferentFlockFilter>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var context = GetNearbyObjectsTransforms(flockAgent);

        // context = filter.Filter(flockAgent, context);

        flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++

        var calculatedMoveSpeed = CalculateMoveSpeed(flockAgent, context);
        calculatedMoveSpeed *= 10f;

        if (calculatedMoveSpeed.sqrMagnitude > flockAgent.ParentFlock.MaxAgentSpeed)
        {
            calculatedMoveSpeed = calculatedMoveSpeed.normalized * flockAgent.ParentFlock.MaxAgentSpeed;
        }

        // flockAgent.Move(calculatedMoveSpeed);
        flockAgent.transform.up = calculatedMoveSpeed;
        flockAgent.transform.position += (Vector3)calculatedMoveSpeed * Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    private Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // all add points together and average
        var cohesionMove = Vector2.zero;

        var count = 0;

        foreach (var item in context)
        {
            // instead of context
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= agent.ParentFlock.SquareSmallRadius)
            {
                cohesionMove += (Vector2)item.position;
                count++;
            }
        }

        if (count != 0)
        {
            cohesionMove /= count;
        }

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        cohesionMove = Vector2.SmoothDamp(
                agent.transform.up,
                cohesionMove,
                ref currentVelocity,
                0.001f
            );

        return cohesionMove;
    }

    private List<Transform> GetNearbyObjectsTransforms(FlockAgent agent)
    {
        return Physics2D
            .OverlapCircleAll(agent.transform.position, agent.ParentFlock.NeighborRadius)
            .Where(x => x != agent.AgentCollider)
            .Select(x => x.transform)
            .ToList();
    }
}