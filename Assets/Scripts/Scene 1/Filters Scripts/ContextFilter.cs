using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_1.Filters_Scripts
{
    public abstract class ContextFilter : ScriptableObject
    {
        public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
    }
}
