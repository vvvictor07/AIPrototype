using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_2
{
    public enum LifeType
    {
        Predator,

        Prey,
    }

    // GameObject must have Collider2D attached
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(LifeCharacteristics))]
    public class Life : MonoBehaviour
    {
        [SerializeField]
        public LifeType Type = LifeType.Prey;

        [SerializeField]
        public LifeCharacteristics Characteristics;

        private new Collider2D collider;

        private Animator animator;

        // private float velocity;
        public void UpdatePosition(Vector2 velocity)
        {
            if (velocity.sqrMagnitude > Characteristics.AgentMaxSpeed)
            {
                velocity = velocity.normalized * Characteristics.AgentMaxSpeed;
            }

            transform.up = (Vector3)velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }

        public List<Transform> GetNearbyObjectsByRadius(float radius)
        {
            return Physics2D
                .OverlapCircleAll(transform.position, radius)
                .Where(x => x != collider)
                .Select(x => x.transform)
                .ToList();
        }

        // Start is called before the first frame update
        protected void Start()
        {
            collider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        protected void Update()
        {
            if (animator != null)
            {
                animator.SetFloat("Minimal distance to life same type", GetMinimalDistanceToLifeSameType());
                animator.SetFloat("Minimal distance to life other type", GetMinimalDistanceToLifeOtherType());
            }
        }

        private float GetMinimalDistanceToLifeSameType()
        {
            var nearbyObjects = GetNearbyObjectsByRadius(Characteristics.NeighborRadius)
                .Where(x => x.GetComponent<Life>() != null && x.GetComponent<Life>().Type == Type)
                .ToList();

            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }

            return Characteristics.NeighborRadius;
        }

        private float GetMinimalDistanceToLifeOtherType()
        {
            var nearbyObjects = GetNearbyObjectsByRadius(Characteristics.NeighborRadius)
                .Where(x => x.GetComponent<Life>() != null && x.GetComponent<Life>().Type != Type)
                .ToList();

            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }

            return Characteristics.NeighborRadius;
        }
    }
}