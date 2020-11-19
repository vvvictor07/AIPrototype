using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

// gameobject must have Collider2D attached
[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock ParentFlock;

    public Collider2D AgentCollider;

    // public Vector2 Velocity;

    private Animator animator;

    public void Initialize(Flock flock)
    {
        ParentFlock = flock;

        // Velocity = Vector2.up;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void UpdatePosition(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > ParentFlock.MaxAgentSpeed)
        {
            velocity = velocity.normalized * ParentFlock.MaxAgentSpeed;
        }

        transform.up = (Vector3)velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void Start()
    {
        AgentCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Minimal distance to another agent in flock", GetMinimalDistanceToAgentInFlock());
        }
    }

    public List<Transform> GetNearbyObjectsByRadius(float radius)
    {
        return Physics2D
            .OverlapCircleAll(transform.position, radius)
            .Where(x => x != AgentCollider)
            .Select(x => x.transform)
            .ToList();
    }

    private float GetMinimalDistanceToAgentInFlock()
    {
        var nearbyObjects = GetNearbyObjectsByRadius(ParentFlock.NeighborRadius)
            .Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock == ParentFlock)
            .ToList();

        return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
    }
}