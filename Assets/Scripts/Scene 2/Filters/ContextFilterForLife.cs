using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Filters
{
    public abstract class ContextFilterForLife : ScriptableObject
    {
        public abstract List<Transform> Filter(List<Transform> original, Life life = null);
    }
}