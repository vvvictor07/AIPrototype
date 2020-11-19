using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

// gameobject must have Collider2D attached
[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock ParentFlock { get; private set; }

    public Collider2D AgentCollider { get; private set; }

    public Vector2 Velocity { get; set; }

    // private Animator animator;

    public void Initialize(Flock flock)
    {
        ParentFlock = flock;

        Velocity = Vector2.up;

        // if (ParentFlock.name.Equals("Red"))
        // {
        //     animator = gameObject.AddComponent<Animator>();
        //     animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/NPCFSM");
        // }
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void UpdatePosition()
    {
        if (Velocity.sqrMagnitude > ParentFlock.MaxAgentSpeed)
        {
            Velocity = Velocity.normalized * ParentFlock.MaxAgentSpeed;
        }

        transform.up = (Vector3)Velocity;
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }

    private void Start()
    {
        AgentCollider = GetComponent<Collider2D>();
    }

    public List<Transform> GetNearbyObjectsByRadius(float radius)
    {
        return Physics2D
            .OverlapCircleAll(transform.position, radius)
            .Where(x => x != AgentCollider)
            .Select(x => x.transform)
            .ToList();
    }
}