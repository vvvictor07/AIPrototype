using System.Collections.Generic;

using UnityEngine;
using Assets.Scripts.Scene_2.Filters;

namespace Assets.Scripts.Scene_2.Behaviors
{
    public abstract class LifeBehavior : ScriptableObject
    {
        [SerializeField]
        public ContextFilterForLife Filter;

        public abstract Vector2 CalculateMoveSpeed(Life life, List<Transform> context);
    }
}