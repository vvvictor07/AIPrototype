using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// gameobject must have Collider2D attached
[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock ParentFlock { get; private set; }

    public Collider2D AgentCollider { get; private set; }

    public void Initialize(Flock flock)
    {
        ParentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void Start()
    {
        AgentCollider = GetComponent<Collider2D>();
    }
}