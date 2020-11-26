using UnityEngine;

namespace Assets.Scripts.Scene_2
{
    [CreateAssetMenu(menuName = "Life/LifeCharacteristics")]
    public class LifeCharacteristics : ScriptableObject
    {
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

        private float avoidanceRadius;

        private float smallRadius;

        public float AvoidanceRadius => avoidanceRadius;

        public float SmallRadius => smallRadius;

        private void Init()
        {
            avoidanceRadius = NeighborRadius * AvoidanceRadiusMultiplier;
            smallRadius = NeighborRadius * SmallRadiusMultiplier;
        }
    }
}