using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1
{
    public class Flock : MonoBehaviour
    {
        public FlockAgent AgentPrefab;

        [Range(1, 500)]
        public int InitialQuantity = 250;

        [Range(1f, 100f)]
        public float DriveFactor = 10f;

        [Range(1f, 100f)]
        public float AgentMaxSpeed = 5f;

        [Range(0f, 10f)]
        public float NeighborRadius = 1.5f;

        [Range(0f, 1f)]
        public float AvoidanceRadiusMultiplier = 0.5f;

        [Range(0f, 1f)]
        public float SmallRadiusMultiplier = 0.2f;

        private const float AgentDensity = 0.01f;

        private float avoidanceRadius;

        private float smallRadius;

        private List<FlockAgent> agents = new List<FlockAgent>();

        public float AvoidanceRadius => avoidanceRadius;

        public float SmallRadius => smallRadius;

        private void Start()
        {
            avoidanceRadius = NeighborRadius * AvoidanceRadiusMultiplier;
            smallRadius = NeighborRadius * SmallRadiusMultiplier;

            // loops for startingCount times
            for (var i = 0; i < InitialQuantity; i++)
            {
                var newAgent = Instantiate(
                        AgentPrefab,
                        Random.insideUnitCircle * InitialQuantity * AgentDensity,
                        Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                        transform
                    );
                newAgent.name = name + " Agent " + i;

                newAgent.Initialize(this);
                agents.Add(newAgent);
            }
        }

        private void Update()
        {
        }
    }
}
