using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_1
{
    // gameobject must have Collider2D attached
    [RequireComponent(typeof(Collider2D))]
    public class FlockAgent : MonoBehaviour
    {
        public Flock ParentFlock;

        public Collider2D AgentCollider;

        private Animator animator;

        public void Initialize(Flock flock)
        {
            ParentFlock = flock;
        }

        public void Move(Vector2 velocity)
        {
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }

        public void UpdatePosition(Vector2 velocity)
        {
            if (velocity.sqrMagnitude > ParentFlock.AgentMaxSpeed)
            {
                velocity = velocity.normalized * ParentFlock.AgentMaxSpeed;
            }

            transform.up = (Vector3)velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }

        public List<Transform> GetNearbyObjectsByRadius(float radius)
        {
            return Physics2D
                .OverlapCircleAll(transform.position, radius)
                .Where(x => x != AgentCollider)
                .Select(x => x.transform)
                .ToList();
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
                animator.SetFloat("Minimal distance to agent in flock", GetMinimalDistanceToAgentInFlock());
                animator.SetFloat("Minimal distance to agent in another flock", GetMinimalDistanceToAgentInAnotherFlock());
            }
        }

        private float GetMinimalDistanceToAgentInFlock()
        {
            var nearbyObjects = GetNearbyObjectsByRadius(ParentFlock.NeighborRadius)
                .Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock == ParentFlock)
                .ToList();

            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }

            return ParentFlock.NeighborRadius;
        }

        private float GetMinimalDistanceToAgentInAnotherFlock()
        {
            var nearbyObjects = GetNearbyObjectsByRadius(ParentFlock.NeighborRadius)
                .Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock != ParentFlock)
                .ToList();

            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }

            return ParentFlock.NeighborRadius;
        }
    }
}
