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
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //direction to the center
        //magnitude will = distance
        Vector2 centerOffet = center - (Vector2)agent.transform.position;
        float t = centerOffet.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffet * t * t;
    }
}
