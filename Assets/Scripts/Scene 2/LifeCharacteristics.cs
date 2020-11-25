using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Life/LifeCharacteristics")]
public class LifeCharacteristics : ScriptableObject
{
    public enum LifeType
    {
        Predator,
        Prey
    }

    [SerializeField]
    private LifeType lifeType;
    
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