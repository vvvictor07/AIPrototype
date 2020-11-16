using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;

    public FlockBehavior behavior;

    [Range(10, 500)]
    public int InitialQuantity = 250;

    [Range(1f, 100f)]
    public float DriveFactor = 10f;

    [Range(1f, 100f)]
    public float MaxAgentSpeed = 5f;

    [Range(1f, 10f)]
    public float NeighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float AvoidanceRadiusMultiplier = 0.5f;

    [Range(0f, 1f)]
    public float SmallRadiusMultiplier = 0.2f;

    private const float AgentDensity = 0.08f;

    private float squareMaxSpeed;

    private float squareNeighborRadius;

    private float squareAvoidanceRadius;

    private float squareSmallRadius;

    private List<FlockAgent> agents = new List<FlockAgent>();

    public float SquareAvoidanceRadius => squareAvoidanceRadius;

    public float SquareSmallRadius => squareSmallRadius;

    private void Start()
    {
        squareMaxSpeed = MaxAgentSpeed * MaxAgentSpeed;
        squareNeighborRadius = NeighborRadius * NeighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * AvoidanceRadiusMultiplier * AvoidanceRadiusMultiplier;
        squareSmallRadius = squareNeighborRadius * SmallRadiusMultiplier * SmallRadiusMultiplier;

        // loops for startingCount times
        for (var i = 0; i < InitialQuantity; i++)
        {
            var newAgent = Instantiate(
                    agentPrefab,
                    Random.insideUnitCircle * InitialQuantity * AgentDensity,
                    Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                    transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        foreach (var agent in agents)
        {
            var context = GetNearbyObjects(agent);

            // testing code
            // agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++
            var calculatedMoveSpeed = behavior.CalculateMoveSpeed(agent, context);
            calculatedMoveSpeed *= DriveFactor;

            if (calculatedMoveSpeed.sqrMagnitude > squareMaxSpeed)
            {
                calculatedMoveSpeed = calculatedMoveSpeed.normalized * MaxAgentSpeed;
            }

            agent.Move(calculatedMoveSpeed);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        var context = new List<Transform>();
        var contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, NeighborRadius);

        foreach (var c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}