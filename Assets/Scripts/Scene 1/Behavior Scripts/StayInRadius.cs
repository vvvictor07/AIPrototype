using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadius : FlockBehavior
{
    [SerializeField]
    private Vector2 center;

    [SerializeField]
    private float radius = 15;

    public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
    {
        // direction to the center
        // magnitude will = distance
        var centerOffet = center - (Vector2)agent.transform.position;
        var centerOffetInRadius = centerOffet.magnitude / radius;

        if (centerOffetInRadius < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffet * centerOffetInRadius * centerOffetInRadius;
    }
}