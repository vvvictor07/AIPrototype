using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1
{
    public abstract class FlockBehavior : ScriptableObject
    {
        public abstract Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context);
    }
}
